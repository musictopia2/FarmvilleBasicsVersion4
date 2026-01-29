namespace Phase04Achievements.Services.Achievements;
public class AchievementManager(InventoryManager inventoryManager, 
    WorkshopManager workshopManager,
    WorksiteManager worksiteManager,
    ProgressionManager progressionManager,
    TimedBoostManager timedBoostManager,
    AnimalManager animalManager
    )
{
    private BasicList<AchievementPlanModel> _plans = [];
    private AchievementProfileModel _profile = null!;
    public async Task SetAchievementStyleContextAsync(AchievementServicesContext context, FarmKey farm)
    {
        _plans = await context.AchievementPlanProvider.GetPlanAsync(farm);
        inventoryManager.InventoryAdded += ProcessInventoryAdded;
        inventoryManager.InventoryConsumed += ProcessInventoryConsumed;
        workshopManager.OnWorkshopItemQued += ProcessWorkshopQueAdded;
        worksiteManager.OnRewardPickedUp += ProcessWorksiteRewards;
        progressionManager.OnIncreaseLevel += ProcessLevelIncrease;
        timedBoostManager.OnUsedTimedBoost += ProcessTimedBoost;
        animalManager.OnAnimalCollected += ProcessAnimalCollected;
    }
    private void ProcessInventoryAdded(ItemAmount item)
    {

    }
    private void ProcessInventoryConsumed(ItemAmount item)
    {

    }
    private void ProcessWorkshopQueAdded(string buildingName, string craftedItem)
    {

    }
    private void ProcessWorksiteRewards(string location, ItemAmount reward)
    {

    }
    private void ProcessLevelIncrease(int newLevel)
    {

    }
    private void ProcessTimedBoost(string boostKey, string? OutputAugmentationKey)
    {

    }
    private void ProcessAnimalCollected(string animalName)
    {

    }




}