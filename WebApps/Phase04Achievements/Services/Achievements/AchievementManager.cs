namespace Phase04Achievements.Services.Achievements;
public class AchievementManager(InventoryManager inventoryManager, 
    WorkshopManager workshopManager,
    WorksiteManager worksiteManager,
    ProgressionManager progressionManager,
    TimedBoostManager timedBoostManager,
    AnimalManager animalManager,
    QuestManager questManager
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
        questManager.OnOrderCompleted += ProcessOrderCompleted;
    }
    private async void ProcessInventoryAdded(ItemAmount item)
    {

    }
    private async void ProcessInventoryConsumed(ItemAmount item)
    {

    }
    private async void ProcessWorkshopQueAdded(string buildingName, string craftedItem)
    {

    }
    private async void ProcessWorksiteRewards(string location, ItemAmount reward)
    {

    }
    private async void ProcessLevelIncrease(int newLevel)
    {

    }
    private async void ProcessTimedBoost(string boostKey, string? OutputAugmentationKey)
    {

    }
    private async void ProcessAnimalCollected(string animalName)
    {

    }

    private async void ProcessOrderCompleted(string item)
    {
        //this can double count (because you complete one with an order if that matches) plus for completion of any order.



    }

    public async Task ScenarioCompleted()
    {
        //this would give credit for this achievement.
    }


}