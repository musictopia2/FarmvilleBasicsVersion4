namespace Phase04Achievements.Services.Store;

public class StoreManager(IFarmProgressionReadOnly levelProgression,
    TreeManager treeManager,
    AnimalManager animalManager,
    WorkshopManager workshopManager,
    WorksiteManager worksiteManager,
    CatalogManager catalogManager,
    InventoryManager inventoryManager,
    InstantUnlimitedManager instantUnlimitedManager,
    TimedBoostManager timedBoostManager,
    RentalManager rentalManager
    )
{
    private int _currentLevel;
    private bool _completedGame;
    private EnumCatalogCategory _catalogCategory;
    private IStoreUiStateRepository _storeUiStateRepository = null!;
    private BasicList<CatalogOfferModel> _treesOffers = [];
    private BasicList<CatalogOfferModel> _animalOffers = [];
    private BasicList<CatalogOfferModel> _workshopOffers = [];
    private BasicList<CatalogOfferModel> _worksiteOffers = [];
    private BasicList<CatalogOfferModel> _workerOffers = [];
    private BasicList<CatalogOfferModel> _timedOffers = [];
    private BasicList<CatalogOfferModel> _instantUnlimitedOffers = [];
    private BasicList<CatalogOfferModel> _miscOffers = [];
    public EnumCatalogCategory CurrentCategory => _catalogCategory;
    public async Task SetProgressionStyleContextAsync(StoreServicesContext context)
    {
        _catalogCategory = await context.UiStateRepository.LoadAsync();
        _storeUiStateRepository = context.UiStateRepository;
        _treesOffers = catalogManager.GetAllOffers(EnumCatalogCategory.Tree);
        _animalOffers = catalogManager.GetAllOffers(EnumCatalogCategory.Animal);
        _workshopOffers = catalogManager.GetAllOffers(EnumCatalogCategory.Workshop);
        _worksiteOffers = catalogManager.GetAllOffers(EnumCatalogCategory.Worksite);
        _workerOffers = catalogManager.GetAllOffers(EnumCatalogCategory.Worker);
        _instantUnlimitedOffers = catalogManager.GetAllOffers(EnumCatalogCategory.InstantUnlimited);
        _timedOffers = catalogManager.GetAllOffers(EnumCatalogCategory.TimedBoosts);
        _miscOffers = catalogManager.GetAllOffers(EnumCatalogCategory.Misc);
        levelProgression.Changed += Refresh;
        Refresh();
    }
    public async Task ChoseNewCategoryAsync(EnumCatalogCategory category)
    {
        _catalogCategory = category;
        await _storeUiStateRepository.SaveAsync(_catalogCategory);
    }
    public bool CanAfford(StoreItemRowModel store) => inventoryManager.Has(store.Costs);
    public BasicList<StoreItemRowModel> GetStoreItems()
    {
        if (_completedGame)
        {
            return [];
        }
        if (_catalogCategory == EnumCatalogCategory.Tree)
        {
            return GetTrees();
        }
        if (_catalogCategory == EnumCatalogCategory.Animal)
        {
            return GetAnimals();
        }
        if (_catalogCategory == EnumCatalogCategory.Workshop)
        {
            return GetWorkshops();
        }
        if (_catalogCategory == EnumCatalogCategory.Worker)
        {
            return GetWorkers();
        }
        if (_catalogCategory == EnumCatalogCategory.Worksite)
        {
            return GetWorksites();
        }
        if (_catalogCategory == EnumCatalogCategory.InstantUnlimited)
        {
            return GetInstantUnlimitedItems();
        }
        if (_catalogCategory == EnumCatalogCategory.TimedBoosts)
        {
            return GetTimedOffers();
        }
        if (_catalogCategory == EnumCatalogCategory.Misc)
        {
            return GetMiscOffers();
        }
        throw new CustomBasicException("Not supported yet");
    }
    private BasicList<StoreItemRowModel> GetMiscOffers()
    {
        if (_miscOffers is null || _miscOffers.Count == 0)
        {
            return [];
        }
        BasicList<StoreItemRowModel> rows = [];
        foreach (var offer in _miscOffers)
        {
            int owned = 0;
            if (offer.Quantity > 0)
            {
                //this means needs inventory manager.
                owned = inventoryManager.Get(offer.TargetName);
            }
            bool isLocked = _currentLevel < offer.LevelRequired;
            //not sure otherwise for now
            rows.Add(new()
            {
                Quantity = offer.Quantity,
                OwnedCount = owned,
                TargetName = offer.TargetName,
                Repeatable = true,
                LevelRequired = offer.LevelRequired,
                Category = EnumCatalogCategory.Misc,
                IsLocked = isLocked,
                IsMaxedOut = false,
                TotalPossible = 0,
                Costs = offer.Costs
            });
        }
        return rows;
    }
    private BasicList<StoreItemRowModel> GetTimedOffers()
    {
        if (_timedOffers is null || _timedOffers.Count == 0)
        {
            return [];
        }

        // quantity lookup from profile credits
        var creditLookup = timedBoostManager
            .GetBoosts();
        //.ToDictionary(x => x.BoostKey, x => x.Quantity);

        BasicList<StoreItemRowModel> rows = [];

        foreach (var offer in _timedOffers)
        {
            //int qty = creditLookup.TryGetValue(offer.TargetName, out int q) ? q : 0;
            if (offer.Costs.Count == 0)
            {
                continue;
            }
            var temp = creditLookup.SingleOrDefault(x => x.Duration == offer.Duration && x.BoostKey == offer.TargetName);
            int qty = 0;
            if (temp is not null)
            {
                qty = temp.Quantity;
            }
            bool isLocked = _currentLevel < offer.LevelRequired;

            rows.Add(new StoreItemRowModel
            {
                Category = EnumCatalogCategory.TimedBoosts,
                TargetName = offer.TargetName,

                // For boosts, this is just the min level to buy
                LevelRequired = offer.LevelRequired,

                // Not meaningful for boosts — UI should special-case
                TotalPossible = 0,

                // Quantity owned
                OwnedCount = qty,

                Costs = offer.Costs,
                ReducedBy = offer.ReduceBy,
                IsLocked = isLocked,
                Duration = offer.Duration,
                Repeatable = offer.Repeatable,
                IsMaxedOut = false,
                OutputAugmentationKey = offer.OutputAugmentationKey
            });
        }

        return rows;
    }
    private BasicList<StoreItemRowModel> GetInstantUnlimitedItems()
    {
        var offers = _instantUnlimitedOffers;
        if (offers is null || offers.Count == 0)
        {
            return [];
        }
        var ownedLookup = BuildOwnedLookup(instantUnlimitedManager.UnlockedInstances, x => x);
        return BuildRowsFromTieredOffers(
            offers,
            targetName => ownedLookup.TryGetValue(targetName, out int c) ? c : 0,
            EnumCatalogCategory.InstantUnlimited
        );
    }
    private BasicList<StoreItemRowModel> GetAnimals()
    {
        var offers = _animalOffers;
        if (offers is null || offers.Count == 0)
        {
            return [];
        }
        var ownedLookup = BuildOwnedLookup(animalManager.GetUnlockedAnimals, x => x.Name);
        return BuildRowsFromTieredOffers(
            offers,
            targetName => ownedLookup.TryGetValue(targetName, out int c) ? c : 0,
            EnumCatalogCategory.Animal
        );
    }
    private BasicList<StoreItemRowModel> GetTrees()
    {
        var offers = _treesOffers;
        if (offers is null || offers.Count == 0)
        {
            return [];
        }

        // Build owned counts from the tree manager (Name matches CatalogOfferModel.TargetName)
        var ownedLookup = treeManager.GetUnlockedTrees
            .GroupBy(t => t.TreeName)
            .ToDictionary(g => g.Key, g => g.Count());

        return BuildRowsFromTieredOffers(
            offers,
            targetName => ownedLookup.TryGetValue(targetName, out int c) ? c : 0,
            EnumCatalogCategory.Tree
        );
    }

    private static string GetOfferVariantKey(CatalogOfferModel offer)
    {
        // This should uniquely represent what the player is buying.
        // If Duration differs -> separate entry (your requirement).
        string dur = offer.Duration is null ? "PERM" : $"D{offer.Duration.Value.TotalSeconds:0}";
        string red = offer.ReduceBy is null ? "" : $"R{offer.ReduceBy.Value.TotalSeconds:0}";
        string qty = offer.Quantity <= 1 ? "" : $"Q{offer.Quantity}";
        string aug = string.IsNullOrWhiteSpace(offer.OutputAugmentationKey) ? "" : $"A{offer.OutputAugmentationKey}";
        // Repeatable usually matters for display/limits, but include it anyway to be safe.
        string rep = offer.Repeatable ? "REP" : "ONCE";

        return $"{offer.TargetName}|{dur}|{red}|{qty}|{aug}|{rep}";
    }

    private BasicList<StoreItemRowModel> GetWorkshops()
    {
        var offers = _workshopOffers;
        if (offers is null || offers.Count == 0)
        {
            return [];
        }
        var ownedLookup = workshopManager.GetUnlockedWorkshops
            .GroupBy(t => t.Name)
            .ToDictionary(g => g.Key, g => g.Count());
        return BuildRowsFromTieredOffers(
            offers,
            targetName => ownedLookup.TryGetValue(targetName, out int c) ? c : 0,
            EnumCatalogCategory.Workshop
        );
    }

    private BasicList<StoreItemRowModel> GetWorksites()
    {
        var offers = _worksiteOffers;
        if (offers is null || offers.Count == 0)
        {
            return [];
        }
        var ownedLookup = worksiteManager.GetUnlockedWorksites()
            .GroupBy(t => t)
            .ToDictionary(g => g.Key, g => g.Count());
        return BuildRowsFromTieredOffers(
            offers,
            targetName => ownedLookup.TryGetValue(targetName, out int c) ? c : 0,
            EnumCatalogCategory.Worksite
        );
    }
    private BasicList<StoreItemRowModel> GetWorkers()
    {
        var offers = _workerOffers;
        if (offers is null || offers.Count == 0)
        {
            return [];
        }
        var ownedLookup = worksiteManager.GetAllUnlockedWorkers()
            .GroupBy(t => t)
            .ToDictionary(g => g.Key, g => g.Count());
        return BuildRowsFromTieredOffers(
            offers,
            targetName => ownedLookup.TryGetValue(targetName, out int c) ? c : 0,
            EnumCatalogCategory.Worker
        );
    }
    private static Dictionary<string, int> BuildOwnedLookup<T>(
        IEnumerable<T> items,
        Func<T, string> keySelector)
    {
        return items
            .GroupBy(keySelector)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    private BasicList<StoreItemRowModel> BuildRowsFromTieredOffers(
        BasicList<CatalogOfferModel> offers,
        Func<string, int> ownedCountProvider,
        EnumCatalogCategory category)
    {
        if (offers is null || offers.Count == 0)
        {
            return [];
        }

        BasicList<StoreItemRowModel> rows = [];

        foreach (var group in offers.GroupBy(GetOfferVariantKey))
        {
            

            var tiers = group
                .OrderBy(x => x.LevelRequired)
                .ToBasicList();
            // IMPORTANT: TargetName is still the actual thing being unlocked
            string targetName = tiers.First().TargetName;
            TimeSpan? duration = tiers.First().Duration;
            int ownedCount;
            int totalPossible;
            string? rentalDetails = null;
            EnumRentalState? rentalState = null;
            if (duration is not null)
            {
                ownedCount = 0;
                totalPossible = 0;
                rentalState = rentalManager.GetRentalState(tiers.First().TargetName);
                if (rentalState == EnumRentalState.Active)
                {
                    rentalDetails = rentalManager.GetDurationString(tiers.First().TargetName);
                }
                else
                {
                    rentalDetails = "Collect."; //not enough room for more details.
                }
            }
            else
            {
                ownedCount = ownedCountProvider(targetName);
                totalPossible = tiers.Count;
            }
            

            // maxed out row
            if (ownedCount >= totalPossible && duration is null)
            {
                rows.Add(new StoreItemRowModel
                {
                    TargetName = targetName,
                    TotalPossible = totalPossible,
                    OwnedCount = ownedCount,
                    LevelRequired = 0,
                    Costs = [],
                    IsLocked = false,
                    IsMaxedOut = true,
                    Category = category
                });
                continue;
            }

            // NEW: hide entire target if progression should have granted a free tier
            if (HasEligibleFreeTierNotYetOwned(tiers, ownedCount))
            {
                continue;
            }
            CatalogOfferModel nextTier;

            if (duration is null)
            {
                nextTier = tiers[ownedCount];
            }
            else
            {
                nextTier = tiers.First();
            }


            // OPTIONAL: if you never want free tiers to appear in store at all:
            if (IsFreeTier(nextTier))
            {
                continue;
            }
            bool isLocked = _currentLevel < nextTier.LevelRequired;
            rows.Add(new StoreItemRowModel
            {
                TargetName = nextTier.TargetName,
                TotalPossible = totalPossible,
                OwnedCount = ownedCount,
                LevelRequired = nextTier.LevelRequired,
                Costs = nextTier.Costs,
                IsLocked = isLocked,
                IsMaxedOut = false,
                Category = category,
                Duration = nextTier.Duration,
                RentalDetails = rentalDetails,
                RentalState = rentalState
            });
        }

        return rows;
    }

    private static bool IsFreeTier(CatalogOfferModel tier)
    {
        // Adjust if you have an explicit flag.
        return tier.Costs.Count == 0;
    }

    //hopefully still works
    private bool HasEligibleFreeTierNotYetOwned(
        BasicList<CatalogOfferModel> tiers,
        int ownedCount)
    {
        // Scan from next unowned tier forward.
        for (int i = ownedCount; i < tiers.Count; i++)
        {
            var t = tiers[i];

            bool eligibleByLevel = _currentLevel >= t.LevelRequired;
            bool free = IsFreeTier(t);

            // If there's a FREE tier the player is eligible for,
            // but ownedCount says we don't have it, progression is responsible.
            if (free && eligibleByLevel)
            {
                return true;
            }

            // If it’s not eligible yet, or it’s paid, stop scanning.
            // We only care about "eligible free tiers we somehow don't own".
            if (eligibleByLevel == false || free == false)
            {
                return false;
            }
        }

        return false;
    }

    private void Refresh()
    {
        _currentLevel = levelProgression.CurrentLevel;
        _completedGame = levelProgression.CompletedGame;
    }
    public bool CanAcquire(StoreItemRowModel store)
    {
        if (inventoryManager.Has(store.Costs) == false)
        {
            return false;
        }
        if (store.IsMaxedOut)
        {
            return false;
        }
        if (store.LevelRequired > _currentLevel)
        {
            return false;
        }
        if (store.IsFree)
        {
            return false; //because the level progression should had handled this.
        }
        if (store.Duration is null)
        {
            return true;
        }
        return rentalManager.CanRent(store);
    }
    public async Task AcquireAsync(StoreItemRowModel store)
    {
        if (CanAcquire(store) == false)
        {
            throw new CustomBasicException("Unable to acquire this.  Should had called CanAcquire first");
        }

        if (store.Duration is not null && store.Category != EnumCatalogCategory.Misc && store.Category != EnumCatalogCategory.TimedBoosts)
        {
            await rentalManager.RentAsync(store);
            FinishAcquiring(store);
            return;
        }

        //now needs to actually unlock the items.
        if (store.Category == EnumCatalogCategory.Tree)
        {
            treeManager.UnlockTreePaidFor(store);

        }
        if (store.Category == EnumCatalogCategory.Animal)
        {
            //if rentals are needed, will rethink.
            animalManager.UnlockAnimalPaidFor(store);
            FinishAcquiring(store);
            return;
        }
        if (store.Category == EnumCatalogCategory.Workshop)
        {
            workshopManager.UnlockWorkshopPaidFor(store);
            FinishAcquiring(store);
            return;
        }
        if (store.Category == EnumCatalogCategory.Worksite)
        {
            worksiteManager.UnlockWorksitePaidFor(store);
            FinishAcquiring(store);
            return;
        }
        if (store.Category == EnumCatalogCategory.Worker)
        {
            await worksiteManager.UnlockWorkerAcquiredAsync(store);
            FinishAcquiring(store);
            return;
        }
        if (store.Category == EnumCatalogCategory.InstantUnlimited)
        {
            await instantUnlimitedManager.SetLockStateAsync(store.TargetName, true);
            FinishAcquiring(store);
            return;
        }
        if (store.Category == EnumCatalogCategory.TimedBoosts)
        {
            var offer = _timedOffers.Single(x => x.TargetName == store.TargetName && x.Duration == store.Duration);
            await timedBoostManager.GrantCreditAsync(offer);
            FinishAcquiring(store);
            return;
        }
        if (store.Category == EnumCatalogCategory.Misc)
        {
            if (store.Quantity > 0)
            {
                //this means you increase the inventory by this amount.
                inventoryManager.Add(store.TargetName, store.Quantity);
                FinishAcquiring(store);
                return;
            }
            throw new CustomBasicException("Needs to figure out other cases for misc stuff");
        }
    }
    private void FinishAcquiring(StoreItemRowModel store)
    {
        inventoryManager.Consume(store.Costs);
    }
}