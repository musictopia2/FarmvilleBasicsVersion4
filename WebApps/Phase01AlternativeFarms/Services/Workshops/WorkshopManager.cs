namespace Phase01AlternativeFarms.Services.Workshops;
public class WorkshopManager(InventoryManager inventory,
    IBaseBalanceProvider baseBalanceProvider,
    ItemRegistry itemRegistry,
    TimedBoostManager timedBoostManager,
    OutputAugmentationManager outputAugmentationManager
    )
{
    private IWorkshopRespository _workshopRespository = null!;
    private bool _automateCollection;
    private readonly BasicList<WorkshopInstance> _workshops = [];
    private BasicList<WorkshopRecipe> _recipes = [];
    public event Action? OnWorkshopsUpdated;
    private readonly Lock _lock = new();
    private bool _needsSaving;
    private DateTime _lastSave = DateTime.MinValue;
    private double _multiplier;
    public event Action<ItemAmount>? OnAugmentedOutput;

    private WorkshopInstance GetWorkshopById(Guid id)
    {
        var workshop = _workshops.SingleOrDefault(t => t.Id == id) ?? throw new CustomBasicException($"Workshop with Id {id} not found.");
        return workshop;
    }
    private WorkshopInstance GetWorkshopById(WorkshopView id) => GetWorkshopById(id.Id);
    public BasicList<WorkshopView> GetUnlockedWorkshops
    {
        get
        {
            lock (_lock)
            {
                BasicList<WorkshopView> output = [];

                _workshops.ForConditionalItems(x => x.Unlocked, t =>
                {
                    WorkshopView summary = new()
                    {
                        Id = t.Id,
                        Name = t.BuildingName,
                        SelectedRecipeIndex = t.SelectedRecipeIndex,
                        IsRental = t.IsRental,
                        Unlocked = t.Unlocked,
                        ReadyCount = t.Queue.Count(x => x.State == EnumWorkshopState.ReadyToPickUpManually)
                    };
                    output.Add(summary);
                });


                return output;
            }
        }
    }
    public bool IsInBuilding(string buildingName, string itemToCheck)
    {
        var recipe = _recipes.Single(x => x.Item == itemToCheck);
        return recipe.BuildingName == buildingName;
    }
    public string? GetBuilding(string itemToCheck)
    {
        var recipe = _recipes.Single(x => x.Item == itemToCheck);

        if (_workshops.Any(x => x.BuildingName == recipe.BuildingName && x.Unlocked))
        {
            return null;
        }

        return recipe.BuildingName;

        //if (_workshops.Any(x => x.Unlocked && x.))
        //var workshop = _workshops.FirstOrDefault(x => x.Unlocked == false && )
    }

    //if you purchase, must make sure all proper items are unlocked like it should had (?)

    public bool Unlocked(WorkshopView workshop) => GetWorkshopById(workshop).Unlocked;
    public void CompleteSingleActiveJobImmediately(WorkshopView workshop)
    {
        if (inventory.Has(CurrencyKeys.FinishSingleWorkshop, 1) == false)
        {
            throw new CustomBasicException("You do not have any finish single workshop consumables left.  Should had called inventory.Has function");
        }
        CompleteActiveJobImmediately(workshop);
        inventory.Consume(CurrencyKeys.FinishSingleWorkshop, 1);
    }

    public void CompleteAllJobsImmediately()
    {
        if (inventory.Has(CurrencyKeys.FinishAllWorkshops, 1) == false)
        {
            throw new CustomBasicException("You do not have any finish all workshop consumables left.");
        }
        foreach (var item in _workshops)
        {
            CompleteAllJobsImmediately(item);
        }
        inventory.Consume(CurrencyKeys.FinishAllWorkshops, 1);
    }


    private void CompleteAllJobsImmediately(WorkshopInstance workshop)
    {
        lock (_lock)
        {
            if (workshop.Queue.Count == 0)
            {
                return;
            }

            var now = DateTime.Now;

            // CASE 1: Manual collection policy -> mark EVERYTHING ready, do not remove anything.
            if (_automateCollection == false)
            {
                foreach (var job in workshop.Queue)
                {
                    job.ReadyForManualPickup();
                }

                _needsSaving = true;
                NotifyWorkshopsUpdated();
                return;
            }

            // CASE 2: Automatic collection policy -> attempt to collect everything in-order.
            // Stop as soon as inventory can’t accept the next output.
            while (workshop.Queue.Any(x => x.State != EnumWorkshopState.ReadyToPickUpManually))
            {
                // Ensure we have an "active" job (for consistency with your model),
                // but we will still finish + collect instantly.
                var active = workshop.Queue.FirstOrDefault(j => j.State == EnumWorkshopState.Active);
                if (active == null)
                {
                    var next = workshop.Queue.FirstOrDefault(j => j.State == EnumWorkshopState.Waiting);
                    if (next == null)
                    {
                        return;
                    }
                    next.Start();
                    active = next;
                    _needsSaving = true;
                }

                // Ensure augmentation promise exists if applicable (so we can roll correctly)
                if (active.OutputPromise is null)
                {
                    bool promised = active.RunPossibleAugmentation();
                    if (promised)
                    {
                        _needsSaving = true;
                    }
                }

                // If we can't add, STOP immediately.
                // (You can optionally flip to manual-ready here so UI explains why it stopped.)
                if (CanAddToInventory(active) == false)
                {
                    // Optional: make it obvious to the player why it halted.
                    active.ReadyForManualPickup();
                    _needsSaving = true;
                    NotifyWorkshopsUpdated();
                    continue;
                    //return; //if they don't have enough for all of it, can go to waste.
                }

                // Add output, respecting augmentation rules
                if (active.OutputPromise is null)
                {
                    inventory.Add(active.Recipe.Output.Item, active.Recipe.Output.Amount);
                }
                else
                {
                    if (rs1.RollHit(active.OutputPromise.Chance))
                    {
                        inventory.Add(active.Recipe.Output.Item, active.Recipe.Output.Amount + 1);
                        OnAugmentedOutput?.Invoke(active.Recipe.Output);
                    }
                    else
                    {
                        inventory.Add(active.Recipe.Output.Item, active.Recipe.Output.Amount);
                    }
                }

                // Complete + remove, then loop for next job
                active.Complete();
                workshop.Queue.RemoveSpecificItem(active);
                _needsSaving = true;
            }

            NotifyWorkshopsUpdated();
        }
    }


    private void CompleteActiveJobImmediately(WorkshopView summary)
    {
        lock (_lock)
        {
            var workshop = GetWorkshopById(summary);

            var active = workshop.Queue.FirstOrDefault(x => x.State == EnumWorkshopState.Active);
            if (active is null)
            {
                return;
            }

            var now = DateTime.Now;

            // Push StartedAt back so that next tick sees elapsed >= DurationForProcessing
            // Add a tiny buffer so you don't lose to timing/precision.
            var buffer = TimeSpan.FromMilliseconds(50);
            active.UpdateStartedAt(now - active.DurationForProcessing - buffer);


        }
    }

    private void ApplyPowerGloveToActiveJob(WorkshopView summary, int used, TimeSpan reduceByPerUse)
    {
        if (used <= 0)
        {
            return;
        }

        lock (_lock)
        {
            var workshop = GetWorkshopById(summary);

            // Only makes sense on an ACTIVE job (matches your earlier design: button only when active)
            var active = workshop.Queue.FirstOrDefault(x => x.State == EnumWorkshopState.Active);
            if (active is null || active.StartedAt is null)
            {
                return;
            }

            TimeSpan totalReduce = reduceByPerUse * used;

            // Shift the start earlier => increases elapsed => finishes sooner
            active.UpdateStartedAt(active.StartedAt.Value - totalReduce);

            _needsSaving = true;
        }

        //no need for the other (because the next second, should be updated).
    }

    public void UsePowerGlove(WorkshopView workshop, int howMany)
    {
        if (inventory.Has(CurrencyKeys.PowerGloveWorkshop, howMany) == false)
        {
            throw new CustomBasicException("Don't have enough power gloves.  Should had called the inventorymanager.Has function");
        }
        ApplyPowerGloveToActiveJob(workshop, howMany, PowerGloveRegistry.ReduceBy);
        inventory.Consume(CurrencyKeys.PowerGloveWorkshop, howMany);
    }

    public Guid StartRental(StoreItemRowModel rental)
    {
        if (rental.Category != EnumCatalogCategory.Workshop)
        {
            throw new CustomBasicException("Only workshops can be rented");
        }
        var instance = _workshops.LastOrDefault(x => x.BuildingName == rental.TargetName && x.Unlocked == false)
            ?? throw new CustomBasicException("No locked building available to rent");
        instance.Unlocked = true;
        instance.IsRental = true; //so later can lock the proper one.  also ui can show the details for it as well.
        _needsSaving = true;
        return instance.Id;
    }
    
    public bool CanDeleteRental(Guid id)
    {
        WorkshopInstance instance = _workshops.Single(x => x.Id == id);
        if (instance.Unlocked == false)
        {
            return true;
        }
        if (instance.Queue.Count == 0)
        {
            instance.Unlocked = false;
            _needsSaving = true;
            NotifyWorkshopsUpdated();
            return false; //try again in a second.
        }
        if (instance.IsRental == false)
        {
            instance.IsRental = true; //implies its a rental.
            NotifyWorkshopsUpdated();
            _needsSaving = true;
        }
        return false;
    }

    public void DoubleCheckActiveRental(Guid id)
    {
        WorkshopInstance instance = _workshops.Single(x => x.Id == id);
        if (instance.IsRental == false)
        {
            instance.IsRental = true;
            NotifyWorkshopsUpdated();
            _needsSaving = true;
        }
        if (instance.Unlocked == false)
        {
            instance.Unlocked = true;
            NotifyWorkshopsUpdated();
            _needsSaving = true;
        }
        
    }


    public void UnlockWorkshopPaidFor(StoreItemRowModel store)
    {
        if (store.Category != EnumCatalogCategory.Workshop)
        {
            throw new CustomBasicException("Only workshops can be paid for");
        }
        var item = _workshops.First(x => x.Unlocked == false && x.BuildingName == store.TargetName && x.IsRental == false);
        item.Unlocked = true;
        _needsSaving = true;
    }
    public void ApplyWorkshopProgressionOnLevelUnlocks(BasicList<ItemUnlockRule> rules, BasicList<CatalogOfferModel> offers, int level)
    {
        //only unlock current level.
        var modify = rules.Where(x => x.LevelRequired == level);
        bool changed = false;
        foreach (var craftedItem in modify)
        {
            WorkshopRecipe recipe = _recipes.Single(x => x.Item == craftedItem.ItemName);
            var list = _workshops.Where(x => x.BuildingName == recipe.BuildingName);
            foreach (var item in list)
            {
                var fins = item.SupportedItems.Single(x => x.Name == craftedItem.ItemName);
                fins.Unlocked = true;
                changed = true;
            }
        }
        var offer = offers.FirstOrDefault(x => x.LevelRequired == level);
        if (offer is not null)
        {
            var workshop = _workshops.First(x => x.BuildingName == offer.TargetName);
            workshop.Unlocked = true;
            changed = true;

        }
        if (changed)
        {
            _needsSaving = true;
        }

    }

    public int GetCapcity(WorkshopView summary)
    {
        WorkshopInstance workshop = GetWorkshopById(summary);
        return workshop.Capacity;
    }
    public void UpdateCapacity(WorkshopView summary, int capacity)
    {
        WorkshopInstance workshop = GetWorkshopById(summary);
        workshop.Capacity = capacity;
        _needsSaving = true;
    }
    public CraftingSummary? GetSingleCraftedItem(WorkshopView summary, int index)
    {
        lock (_lock)
        {
            WorkshopInstance workshop = GetWorkshopById(summary);
            //was one based.
            int reals = index - 1;
            if (reals < 0)
            {
                return null;
            }
            try
            {
                CraftingJobInstance job = workshop.Queue[reals];
                CraftingSummary craftSummary = new()
                {
                    Id = job.Id,
                    Name = job.Recipe.Item,
                    State = job.State,
                    ReadyTime = job.State == EnumWorkshopState.Waiting
                            ? "Waiting"
                            : job.ReadyTime?.GetTimeCompact!
                };
                return craftSummary;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }

    public void StartCraftingJob(WorkshopView summary, string item)
    {
        lock (_lock)
        {
            if (CanCraft(summary, item) == false)
            {
                throw new CustomBasicException("Unable to craft.  Should had ran the CanCraft first");
            }
            WorkshopRecipe recipe = _recipes.Single(x => x.Item == item);
            inventory.Consume(recipe.Inputs);
            TimeSpan reduced = timedBoostManager.GetReducedTime(summary.Name);
            CraftingJobInstance job = new(recipe, _multiplier, reduced, timedBoostManager, outputAugmentationManager);
            WorkshopInstance workshop = GetWorkshopById(summary);
            workshop.ReducedBy = reduced; //i think this too.
            workshop.Queue.Add(job);
            _needsSaving = true;
        }
    }
    public WorkshopView? SearchForWorkshop(string searchFor)
    {
        // Find the recipe that produces the desired item
        WorkshopRecipe? target = _recipes.FirstOrDefault(x => x.Item == searchFor); //you may have more than one.   if more than one, has to choose the first one.  you are on your own from here.
        if (target is null)
        {
            return null;
        }

        // Find the workshop instance that owns that recipe
        WorkshopInstance t = _workshops.First(x => x.BuildingName == target.BuildingName); //has to be first now because you can have more than one workshop with the same name.

        // IMPORTANT: compute the index inside that workshop's recipe list
        // Use the SAME ordering concept your UI relies on.
        var workshopRecipes = _recipes
            .Where(r => r.BuildingName == t.BuildingName)
            .ToList();

        int index = workshopRecipes.FindIndex(r => r.Item == searchFor);
        if (index >= 0)
        {
            // Persist selection so when UI loads it already matches
            t.SelectedRecipeIndex = index;
        }



        return new WorkshopView
        {
            Id = t.Id,
            Name = t.BuildingName,
            SelectedRecipeIndex = index,
            ReadyCount = t.Queue.Count(x => x.State == EnumWorkshopState.ReadyToPickUpManually)
        };
    }
    public void UpdateSelectedRecipe(WorkshopView id, int selectedIndex)
    {
        var workshop = GetWorkshopById(id);
        workshop.SelectedRecipeIndex = selectedIndex;
        //OnWorkshopsUpdated?.Invoke();
        _needsSaving = true;
    }


    private void NotifyWorkshopsUpdated()
    {
        OnWorkshopsUpdated?.Invoke();
    }

    public BasicList<WorkshopRecipeSummary> GetRecipesForWorkshop(WorkshopView summary)
    {
        // _multiplier should be the CURRENT workshop time multiplier (<= 1.0)
        double m = _multiplier;
        TimeSpan timeReduction = timedBoostManager.GetReducedTime(summary.Name);
        var firstList = _recipes.Where(x => x.BuildingName == summary.Name).ToBasicList();
        BasicList<WorkshopRecipeSummary> output = [];



        foreach (var item in firstList)
        {
            var workshop = _workshops.First(x => x.BuildingName == item.BuildingName);
            var nexts = workshop.SupportedItems.Single(x => x.Name == item.Item);

            TimeSpan duration = item.Duration - timeReduction;

            WorkshopRecipeSummary fins = new()
            {
                Duration = duration.Apply(m),
                Inputs = item.Inputs,
                Output = item.Output,
                Item = item.Item,
                Unlocked = nexts.Unlocked
            };
            output.Add(fins);
        }
        return output;

    }
    public bool AnyInQueue(WorkshopView summary)
    {
        lock (_lock)
        {
            WorkshopInstance workshop = GetWorkshopById(summary);
            return workshop.Queue.Count != 0;
        }
    }
    public BasicList<CraftingSummary> GetItemsBeingCrafted(WorkshopView summary)
    {
        lock (_lock)
        {
            WorkshopInstance workshop = GetWorkshopById(summary);
            BasicList<CraftingSummary> output = [];
            foreach (var job in workshop.Queue)
            {
                string readyTime = job.State switch
                {
                    EnumWorkshopState.ReadyToPickUpManually => "Ready",
                    EnumWorkshopState.Waiting => "Waiting",
                    EnumWorkshopState.Active => job.ReadyTime?.GetTimeString ?? "",
                    _ => ""
                };

                output.Add(new CraftingSummary
                {
                    Id = job.Id,
                    Name = job.Recipe.Item,
                    State = job.State,
                    ReadyTime = readyTime
                });
            }
            return output;
        }
    }
    public bool CanCraft(WorkshopView summary, string item)
    {
        lock (_lock)
        {
            WorkshopRecipe recipe = _recipes.Single(x => x.Item == item);
            if (recipe.BuildingName != summary.Name)
            {
                return false;
            }
            WorkshopInstance workshop = GetWorkshopById(summary);
            if (workshop.CanAccept(recipe) == false)
            {
                return false;
            }
            return inventory.Has(recipe.Inputs);
        }
    }

    public bool CanPickupManually(WorkshopView summary)
    {
        lock (_lock)
        {
            WorkshopInstance workshop = GetWorkshopById(summary);
            return workshop.Queue.Any(x => x.State == EnumWorkshopState.ReadyToPickUpManually);
        }
    }
    public bool CanAddToInventory(WorkshopView summary)
    {
        WorkshopInstance workshop = GetWorkshopById(summary);
        return CanAddToInventory(workshop);
    }
    private bool CanAddToInventory(WorkshopInstance workshop)
    {
        CraftingJobInstance active = workshop.Queue.First(x => x.State == EnumWorkshopState.ReadyToPickUpManually);
        return CanAddToInventory(active);
        //return inventory.CanAdd(active.Recipe.Output);
    }
    private bool CanAddToInventory(CraftingJobInstance active)
    {

        if (active.OutputPromise is null)
        {
            return inventory.CanAdd(active.Recipe.Output);
        }
        return inventory.CanAdd(active.Recipe.Output.Item, active.Recipe.Output.Amount + 1);
    }
    //private bool CanAddToInventory(CraftingJobInstance active) => inventory.CanAdd(active.Recipe.Output);
    public void PickupManually(WorkshopView summary)
    {
        lock (_lock)
        {
            WorkshopInstance workshop = GetWorkshopById(summary);
            if (CanAddToInventory(workshop) == false)
            {
                throw new CustomBasicException("Should had used the CanAddToInventory because you were over the barn limits");
            }
            CraftingJobInstance active = workshop.Queue.First(x => x.State == EnumWorkshopState.ReadyToPickUpManually);
            workshop.Queue.RemoveSpecificItem(active);
            //save something here too.
            
            if (active.OutputPromise is null || rs1.RollHit(active.OutputPromise.Chance) == false)
            {
                inventory.Add(active.Recipe.Output.Item, active.Recipe.Output.Amount);
                _needsSaving = true;
                NotifyWorkshopsUpdated();
                return;
            }
            OnAugmentedOutput?.Invoke(active.Recipe.Output);
            inventory.Add(active.Recipe.Output.Item, active.Recipe.Output.Amount);
            inventory.Add(active.Recipe.Output.Item, active.Recipe.Output.Amount);
            _needsSaving = true;
            NotifyWorkshopsUpdated();
        }
    }

    

    public async Task SetStyleContextAsync(WorkshopServicesContext context, FarmKey farm)
    {
        if (_workshopRespository != null)
        {
            throw new InvalidOperationException("Repository Already set");
        }
        BaseBalanceProfile profile = await baseBalanceProvider.GetBaseBalanceAsync(farm);
        _multiplier = profile.WorkshopTimeMultiplier;
        _workshopRespository = context.WorkshopRespository;
        _automateCollection = await context.WorkshopCollectionPolicy.IsAutomaticAsync();
        _recipes = await context.WorkshopRegistry.GetWorkshopRecipesAsync();
        foreach (var item in _recipes)
        {
            itemRegistry.Register(new(item.Output.Item, EnumInventoryStorageCategory.Barn, EnumInventoryItemCategory.Workshops));
        }
        var ours = await context.WorkshopRespository.LoadAsync();
        _workshops.Clear();
        foreach (var item in ours)
        {
            WorkshopInstance workshop = new()
            {
                BuildingName = item.Name
            };
            workshop.Load(item, _recipes, _multiplier, timedBoostManager, outputAugmentationManager);
            _workshops.Add(workshop);
        }
    }
    public async Task UpdateTickAsync()
    {
        _workshops.ForConditionalItems(x => x.SupportedItems.Any(x => x.Unlocked), ProcessBuilding);
        await SaveWorkshopsAsync();
    }
    private void ProcessBuilding(WorkshopInstance workshop)
    {

        // Find active job or start one
        var active = workshop.Queue.FirstOrDefault(j => j.State == EnumWorkshopState.Active);
        if (active == null)
        {
            var next = workshop.Queue.FirstOrDefault(j => j.State == EnumWorkshopState.Waiting);
            if (next != null)
            {
                next.Start();
                _needsSaving = true;
            }
            active = next;
        }

        if (active is null)
        {
            return;
        }
        var now = DateTime.Now;
        var elapsed = now - active.StartedAt!.Value;
        bool rets = active.RunPossibleAugmentation(); //i think if there is something active, must show here.
        if (rets)
        {
            _needsSaving = true;
        }
        while (active != null && elapsed >= active.DurationForProcessing)
        {
            // Consume time for this job
            elapsed -= active.DurationForProcessing;
            if (_automateCollection)
            {
                if (CanAddToInventory(active))
                {
                    //if you are doing automatically, no toasts (no toasts should be here).

                    if (active.OutputPromise is null)
                    {
                        inventory.Add(active.Recipe.Output.Item, active.Recipe.Output.Amount);

                    }
                    else
                    {
                        if (rs1.RollHit(active.OutputPromise.Chance))
                        {
                            //because of augmentation, we add one more and you were successful.
                            inventory.Add(active.Recipe.Output.Item, active.Recipe.Output.Amount + 1);
                        }
                        else
                        {
                            inventory.Add(active.Recipe.Output.Item, active.Recipe.Output.Amount);
                        }
                    }
                    active.Complete();
                    workshop.Queue.RemoveSpecificItem(active);
                    
                    _needsSaving = true;
                }
            }
            else
            {
                active.ReadyForManualPickup();
                NotifyWorkshopsUpdated();
                _needsSaving = true;
                return; // stop processing until player picks up
            }

            // Start next job immediately
            var next = workshop.Queue.FirstOrDefault(j => j.State == EnumWorkshopState.Waiting);
            if (next == null)
            {
                return;
            }

            next.Start();
            active = next;
            active.UpdateStartedAt(now - elapsed);
            _needsSaving = true;
        }
    }
    private async Task SaveWorkshopsAsync()
    {
        bool doSave = false;

        lock (_lock)
        {
            if (_needsSaving && DateTime.Now - _lastSave > GameRegistry.SaveThrottle)
            {
                _needsSaving = false;
                _lastSave = DateTime.Now;
                doSave = true;
            }
        }

        if (doSave == false)
        {
            return;
        }
        BasicList<WorkshopAutoResumeModel> models;
        lock (_lock)
        {
            models = _workshops
                .Select(w => w.GetWorkshopForSaving)
                .ToBasicList();
        }

        await _workshopRespository.SaveAsync(models);
    }


}