namespace Phase02AdvancedUpgrades.Services.Upgrades;
public class UpgradeManager(InventoryManager inventoryManager,
    IInventoryProfile inventoryProfile,
    CropManager cropManager,
    AnimalManager animalManager,
    TreeManager treeManager,
    WorkshopManager workshopManager //i think i need workshop manager for this.
    )
{
    //this focuses on upgrades but are cross cutting.

    //this is different because no tick.

    private InventoryStorageUpgradePlanModel _inventoryPlan = null!;
    private BasicList<WorkshopCapacityUpgradePlanModel> _workshopCapacities = [];
    private InventoryStorageProfileModel _inventoryStorageProfile = null!;
    private BasicList<WorkshopAdvancedUpgradeRuleModel> _workshopUpgrades = [];
    private BasicList<AdvancedUpgradePlanModel> _advancedUpgrades = [];
    public async Task SetInventoryStyleContextAsync(UpgradeServicesContext context,
        InventoryStorageProfileModel storage
        , FarmKey farm)
    {
        //this is where i set everything up.
        //start out with just upgrades to the barn/silo.  later will upgrade other things.
        _inventoryPlan = await context.InventoryStorageUpgradePlanProvider.GetPlanAsync(farm);
        _inventoryStorageProfile = storage;
        _workshopCapacities = await context.WorkshopCapacityUpgradePlanProvider.GetPlansAsync(farm);
        _workshopUpgrades = await context.WorkshopAdvancedUpgradePlanProvider.GetPlansAsync(farm);
        _advancedUpgrades = await context.AdvancedUpgradePlanProvider.GetPlansAsync(farm);
    }
    public bool HasAdvancedUpgrades => _advancedUpgrades.Any(); //if there is none, then no upgrades (like the coin farm).
    public bool IsWorkshopUpgradesMaxedOut(WorkshopView workshop)
    {
        int level = WorkshopCurrentLevel(workshop);
        int maxCount = _advancedUpgrades.Count(x => x.Category == EnumAdvancedUpgradeTrack.Workshop);
        if (level + 1 == maxCount)
        {
            return true;
        }
        if (level + 1 < maxCount)
        {
            return false;
        }
        throw new CustomBasicException("Not sure");
    }
    public int WorkshopCurrentLevel(WorkshopView workshop) => workshopManager.GetLevel(workshop);
    public int WorkshopNextLevelRequirement(WorkshopView workshop)
    {
        var item = _workshopUpgrades.Single(x => x.BuildingName == workshop.Name);
        int level = WorkshopCurrentLevel(workshop);
        level++;
        return item.TierLevelRequired[level - 2];
    }
    public double? GetWorkshopSpeedBoost(int level)
    {
        if (level == 1)
        {
            return null;
        }
        var item = GetAdvancedUpgrade(EnumAdvancedUpgradeTrack.Workshop, level);
        return item.SpeedBonus;
    }
    private AdvancedUpgradeTier GetAdvancedUpgrade(EnumAdvancedUpgradeTrack category, int level)
    {
        return _advancedUpgrades.Single(x => x.Category == category).Tiers[level - 2];
    }
    public Dictionary<string, int> GetWorkshopUpgradeCost(WorkshopView workshop)
    {
        int level = WorkshopCurrentLevel(workshop);
        var item = GetAdvancedUpgrade(EnumAdvancedUpgradeTrack.Workshop, level);
        return item.Cost;
    }
    public int CropCurrentLevel(string crop) => cropManager.Level(crop);
    public int AnimalCurrentLevel(AnimalView animal) => animalManager.Level(animal);
    public int TreeCurrentLevel(TreeView tree) => treeManager.Level(tree);
    public Dictionary<string, int> GetBasicItemsUpgradeCost(int level) //if maxed out, should not do this
    {
        var item = GetAdvancedUpgrade(EnumAdvancedUpgradeTrack.Standard, level);
        return item.Cost;
    }
    public double? GetBasicItemsSpeedBoost(int level, bool isFast)
    {
        if (level == 1)
        {
            return null;
        }
        EnumAdvancedUpgradeTrack category;
        if (isFast)
        {
            category = EnumAdvancedUpgradeTrack.Fastest;
        }
        else
        {
            category = EnumAdvancedUpgradeTrack.Standard;
        }
        var item = GetAdvancedUpgrade(category, level);
        return item.SpeedBonus;
    }

    public bool IsBasicItemUpgradesMaxedOut(int level)
    {
        int maxCount = _advancedUpgrades.Count(x => x.Category == EnumAdvancedUpgradeTrack.Standard); //they both have the same levels
        if (level + 1 == maxCount)
        {
            return true;
        }
        if (level + 1 < maxCount)
        {
            return false;
        }
        throw new CustomBasicException("Not sure");
    }

    public bool IsWorkshopAtMaximumCapacity(WorkshopView workshop)
    {
        int capacity = workshopManager.GetCapcity(workshop);
        var item = _workshopCapacities.Single(x => x.WorkshopName == workshop.Name);
        if (capacity >= item.Upgrades.Count)
        {
            return true; //you are maxed out.
        }
        return false;
    }
    public int NextWorkshopCapacityCoinCost(WorkshopView workshop)
    {
        int capacity = workshopManager.GetCapcity(workshop);

        var item = _workshopCapacities.Single(x => x.WorkshopName == workshop.Name);

        var temp = item.Upgrades[capacity];
        return GetCoinCost(temp);
    }
    public bool CanUpgradeWorkshopCapacity(WorkshopView workshop)
    {
        if (IsWorkshopAtMaximumCapacity(workshop))
        {
            return false;
        }
        int capacity = workshopManager.GetCapcity(workshop);
        var item = _workshopCapacities.Single(x => x.WorkshopName == workshop.Name);
        var temp = item.Upgrades[capacity];
        return CanAfford(temp);
    }
    public void UpgradeWorkshopCapacity(WorkshopView workshop)
    {
        if (CanUpgradeWorkshopCapacity(workshop) == false)
        {
            throw new CustomBasicException("Unable to upgrade the workshop capacity.  Should had called CanUpgradeWorkshopCapacity");
        }
        int capacity = workshopManager.GetCapcity(workshop);
        var item = _workshopCapacities.Single(x => x.WorkshopName == workshop.Name);
        var temp = item.Upgrades[capacity];
        capacity++;
        workshopManager.UpdateCapacity(workshop, capacity);
        inventoryManager.Consume(temp.Cost);
    }
    
    //if this does not work, then rethink.
    public int NextBarnCoinCost
    {
        get
        {
            var temp = _inventoryPlan.BarnUpgrades[_inventoryStorageProfile.BarnLevel + 1];
            return GetCoinCost(temp);
        }
    }
    private static int GetCoinCost(UpgradeTier tier)
    {
        if (tier.Cost.Count > 1)
        {
            throw new CustomBasicException("Not Just Coin");
        }
        if (tier.Cost.Single().Key != CurrencyKeys.Coin)
        {
            throw new CustomBasicException("This was not coin");
        }
        return tier.Cost.Single().Value;
    }
    public int NextSiloCoinCost
    {
        get
        {
            var temp = _inventoryPlan.SiloUpgrades[_inventoryStorageProfile.SiloLevel + 1];
            return GetCoinCost(temp);
        }
    }
    public bool IsBarnMaxedOut
    {
        get
        {
            if (_inventoryStorageProfile.BarnLevel + 1 >= _inventoryPlan.BarnUpgrades.Count)
            {
                return true;
            }
            return false;
        }
    }
    public bool IsSiloMaxedOut
    {
        get
        {
            if (_inventoryStorageProfile.SiloLevel + 1 >= _inventoryPlan.SiloUpgrades.Count)
            {
                return true;
            }
            return false;
        }
    }
    public int NextBarnCount
    {
        get
        {
            var temp = _inventoryPlan.BarnUpgrades[_inventoryStorageProfile.BarnLevel + 1];
            return temp.Size;
        }
    }
    public int NextSiloCount
    {
        get
        {
            var temp = _inventoryPlan.SiloUpgrades[_inventoryStorageProfile.SiloLevel + 1];
            return temp.Size;
        }
    }
    public bool CanUpgradeBarn
    {
        get
        {
            if (_inventoryStorageProfile.BarnLevel >= _inventoryPlan.BarnUpgrades.Count - 1)
            {
                return false;
            }
            var temp = _inventoryPlan.BarnUpgrades[_inventoryStorageProfile.BarnLevel + 1];


            return CanAfford(temp);

            //cost = _inventoryPlan.BarnUpgrades[_inventoryStorageProfile.BarnLevel].Cost;
        }
    }
    public async Task UpgradeBarnAsync()
    {
        if (CanUpgradeBarn == false)
        {
            throw new CustomBasicException("Cannot upgrade the barn.  should had ran CanUpgradeBarn");
        }
        //do a lookup to see what you can do now
        var temp = _inventoryPlan.BarnUpgrades[_inventoryStorageProfile.BarnLevel + 1];
        _inventoryStorageProfile.BarnLevel++;
        _inventoryStorageProfile.BarnSize = temp.Size;

        inventoryManager.Consume(temp.Cost);
        inventoryManager.UpdateInventoryProfile(_inventoryStorageProfile);
        await inventoryProfile.SaveAsync(_inventoryStorageProfile);
    }

    private bool CanAfford(UpgradeTier tier)
    {
        foreach (var item in tier.Cost)
        {
            int has = inventoryManager.Get(item.Key);
            int required = item.Value;
            if ( required > has )
            {
                return false;
            }
        }
        return true;
    }

    public bool CanUpgradeSilo
    {
        get
        {
            if (_inventoryStorageProfile.SiloLevel >= _inventoryPlan.SiloUpgrades.Count - 1)
            {
                return false;
            }
            var temp = _inventoryPlan.SiloUpgrades[_inventoryStorageProfile.SiloLevel + 1];
            return CanAfford(temp);
        }
    }
    public async Task UpgradeSiloAsync()
    {
        if (CanUpgradeSilo == false)
        {
            throw new CustomBasicException("Cannot upgrade the silo.  should had ran CanUpgradeSilo");
        }
        //do a lookup to see what you can do now
        var temp = _inventoryPlan.SiloUpgrades[_inventoryStorageProfile.SiloLevel + 1];
        _inventoryStorageProfile.SiloLevel++;
        _inventoryStorageProfile.SiloSize = temp.Size;
        inventoryManager.Consume(temp.Cost);
        inventoryManager.UpdateInventoryProfile(_inventoryStorageProfile); //just in case.
        await inventoryProfile.SaveAsync(_inventoryStorageProfile);
    }
}