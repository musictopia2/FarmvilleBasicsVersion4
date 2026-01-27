namespace Phase02AdvancedUpgrades.Components.Custom;
public partial class WorkshopComponent(IToast toast)
{
    [Parameter]
    [EditorRequired]
    public WorkshopView Workshop { get; set; }

    [Parameter]
    public EventCallback RentalExpired { get; set; }


    private BasicList<WorkshopRecipeSummary> _recipes = [];

    [Parameter]
    public EventCallback NextClicked { get; set; }

    [Parameter]
    public EventCallback PreviousClicked { get; set; }
    [Parameter]
    public EventCallback<string> NavigateTo { get; set; }
    private int _capacity;
    private bool _showToast = true;
    private bool _showPowerGloves = false;

    private bool _showConfirmation = false;
    private bool _raisedEvent;
    private string _lastRentalText = "";
    private bool _anyAdvancedUpgrades;

    //private string RentalTimeLeft => RentalManager.GetDurationString(Workshop.Name);

    private string GetConfirmationMessage =>
        $"Are you sure you want to use the finish single workshop to complete this?  You have {InventoryManager.Get(CurrencyKeys.FinishSingleWorkshop)} left";
    private void ConfirmCompleteWorkshop(bool allowed)
    {
        _showConfirmation = false;
        if (allowed)
        {
            WorkshopManager.CompleteSingleActiveJobImmediately(Workshop);
            return;
        }
    }
    private string WorkshopDetails
    {
        get
        {
            if (_anyAdvancedUpgrades == false)
            {
                return Workshop.Name.GetWords;
            }
            return $"{Workshop.Name.GetWords} Level {WorkshopManager.GetLevel(Workshop)}";
        }
    }
    private void ClosePowerGloves()
    {
        _showPowerGloves = false;
    }
    protected override void OnParametersSet()
    {
        _anyAdvancedUpgrades = UpgradeManager.HasAdvancedUpgrades;
        _showToast = true; //good news is when the readycount increases since something is ready from the parent calls this so i actually get desired behavior.
        _raisedEvent = false;
        _recipes = WorkshopManager.GetRecipesForWorkshop(Workshop);
        WorkshopRecipeSummary? extra = _recipes.FirstOrDefault(x => x.Unlocked == false);
        _recipes.RemoveAllAndObtain(x => x.Unlocked == false); //so only shows ones you can do.  needs next future one if any.
        if (extra is not null)
        {
            _recipes.Add(extra);
        }
        if (_recipes.Count > 0)
        {
            Workshop.SelectedRecipeIndex = Math.Clamp(
                Workshop.SelectedRecipeIndex, 0, _recipes.Count - 1
            );
        }

        _capacity = WorkshopManager.GetCapcity(Workshop);
        base.OnParametersSet();
    }

    private bool CanShowPossibleNewCapacity => UpgradeManager.IsWorkshopAtMaximumCapacity(Workshop) == false;

    private void UpgradeWorkshopCapacity()
    {
        if (UpgradeManager.CanUpgradeWorkshopCapacity(Workshop) == false)
        {
            toast.ShowUserErrorToast("Unable to upgrade the workshop capacity because you don't have enough coin");
            return;
        }
        UpgradeManager.UpgradeWorkshopCapacity(Workshop);
    }

    private WorkshopRecipeSummary CurrentRecipe => _recipes[Workshop.SelectedRecipeIndex];
    private string ChosenItem => CurrentRecipe.Item;
    private bool CanCraft => WorkshopManager.CanCraft(Workshop, ChosenItem);
    private void Craft()
    {
        if (CurrentRecipe.Unlocked == false)
        {
            return; //cannto craft because its locked
        }
        if (CanCraft == false)
        {
            return;
        }
        WorkshopManager.StartCraftingJob(Workshop, ChosenItem);
    }
    private int CurrentAmount => InventoryManager.GetInventoryCount(ChosenItem);
    private string DetailText
    {
        get
        {
            if (CurrentRecipe.Unlocked)
            {
                return CurrentRecipe.Duration.GetTimeString;
            }
            return $"Level {ProgressionManager.LevelForCraftedItem(CurrentRecipe.Item)}";
        }
    }
    private bool CanGoUp => Workshop.SelectedRecipeIndex > 0;
    private bool CanGoDown => Workshop.SelectedRecipeIndex < _recipes.Count - 1;

    private void GoUp()
    {
        Workshop.SelectedRecipeIndex--;
        WorkshopManager.UpdateSelectedRecipe(Workshop, Workshop.SelectedRecipeIndex);
    }

    private void GoDown()
    {
        Workshop.SelectedRecipeIndex++;
        WorkshopManager.UpdateSelectedRecipe(Workshop, Workshop.SelectedRecipeIndex);
    }
    protected override Task OnTickAsync()
    {
        if (_raisedEvent)
        {
            return base.OnTickAsync();
        }
        if (Workshop.IsRental)
        {
            if (WorkshopManager.Unlocked(Workshop) == false)
            {
                RentalExpired.InvokeAsync();
                _raisedEvent = true;
                return base.OnTickAsync();
            }
        }
        
        if (Workshop.IsRental)
        {
            _lastRentalText = RentalManager.GetDurationString(Workshop.Name);
        }

        _capacity = WorkshopManager.GetCapcity(Workshop);
        if (WorkshopManager.CanPickupManually(Workshop))
        {
            if (WorkshopManager.CanAddToInventory(Workshop))
            {
                WorkshopManager.PickupManually(Workshop); //since you are already there, no problem.
                _showToast = true;
            }
            else if (_showToast)
            {
                toast.ShowUserErrorToast("Unable to pick up crafted item because the barn is full.  Try discarding or consuming the items");
                _showToast = false;
            }
        }
        else if (_showToast == false)
        {
            _showToast = true;
        }
        return base.OnTickAsync();
    }

    private Dictionary<string, int> FullRequirements
    {
        get
        {
            return CurrentRecipe.Inputs;
        }
    }
    private List<CraftingSummary> GetVisibleQueue()
    {
        var list = new List<CraftingSummary>(_capacity);

        // your slots appear to be 1-based
        for (int slot = 1; slot <= _capacity; slot++)
        {
            var s = WorkshopManager.GetSingleCraftedItem(Workshop, slot);
            if (s is null)
            {
                continue;
            }

            // Hide ready-to-pickup items completely from the queue UI
            if (s.State == EnumWorkshopState.ReadyToPickUpManually)
            {
                continue;
            }

            list.Add(s);
        }

        return list;
    }

    private static string SlotClass(int displaySlot) =>
        displaySlot == 1 ? "queue-slot-active" : "queue-slot-inactive";

    //private bool CanShowStack()

    private static string Image(CraftingSummary craft) => $"/{craft.Name}.png";
    private static string GetTimeText(CraftingSummary craft) => craft.ReadyTime;
    private string WorkshopImage => $"/{Workshop.Name}.png";
    private void OnPowerGloveClicked()
    {
        _showPowerGloves = true;
    }
    private void OnFinishNowClicked()
    {
        if (InventoryManager.Has(CurrencyKeys.FinishSingleWorkshop, 1) == false)
        {
            toast.ShowUserErrorToast("You do not have enough Finish Single Workshop consumable to do this.  Try purchasing more");
            return;
        }
        _showConfirmation = true;
    }
}