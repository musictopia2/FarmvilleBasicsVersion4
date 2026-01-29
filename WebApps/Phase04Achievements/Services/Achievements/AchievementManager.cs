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
    private AchievementProfileModel _profileInfo = null!;
    private IAchievementProfile _profileStorage = null!;
    public async Task SetAchievementStyleContextAsync(AchievementServicesContext context, FarmKey farm)
    {
        _plans = await context.AchievementPlanProvider.GetPlanAsync(farm);
        _profileStorage = context.AchievementProfile;
        _profileInfo = await _profileStorage.LoadAsync();
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
    //because this required a toast, had to do the scenario one even though i did not do the others yet.
    public async Task<bool> ScenarioCompletedAsync()
    {
        //this would give credit for this achievement.
        //must at least do this one.
        var plan = _plans.SingleOrDefault(x => x.CounterKey == AchievementCounterKeys.CompleteScenarios);
        if (plan is null)
        {
            return true;
        }
        _profileInfo.ScenariosCompleted++;
        await _profileStorage.SaveAsync(_profileInfo);
        int coins = CoinsFromExactAchievementTarget(plan, _profileInfo.ScenariosCompleted);
        if (coins > 0)
        {
            inventoryManager.AddCoin(coins);
            return true;
        }
        return false;
    }
    private static int CoinsFromExactAchievementTarget(AchievementPlanModel achievement, int currentCount)
    {
        // NON-REPEATABLE
        if (achievement.Target is not null)
        {
            if (achievement.CoinReward.HasValue == false)
            {
                throw new CustomBasicException("If there is a target, must have reward to go with it");
            }
            if (achievement.Target.Value == currentCount)
            {
                return achievement.CoinReward.Value;
            }
            return 0;
        }
        if (achievement.RepeatAchievementRules is null ||
            achievement.RepeatRewardRules is null)
            {
                throw new CustomBasicException("Must have at least an award for this");
            }

        var targets = achievement.RepeatAchievementRules.FirstTargets;
        var rewards = achievement.RepeatRewardRules.FirstCoinRewards;

        // Safety: keep rules aligned
        if (targets.Count != rewards.Count)
        {
            throw new CustomBasicException(
                "Repeat targets and rewards must have same count");
        }
        // 1️⃣ Exact match against first explicit targets
        for (int i = 0; i < targets.Count; i++)
        {
            if (currentCount == targets[i])
            {
                return rewards[i];
            }
        }
        // 2️⃣ After-first repeating tiers
        //thinks its okay because was accounted for above.
        int lastTarget = targets[^1];
        int increment = achievement.RepeatAchievementRules.IncrementAfterFirst;

        if (currentCount <= lastTarget)
        {
            return 0;
        }

        int delta = currentCount - lastTarget;

        // must land EXACTLY on a tier
        if (delta % increment != 0)
        {
            return 0;
        }

        int tierIndex = (delta / increment) - 1;

        int lastReward = rewards[^1];
        int rewardIncrement =
            achievement.RepeatRewardRules.CoinIncrementAfterFirst;

        return lastReward + ((tierIndex + 1) * rewardIncrement);

    }
}