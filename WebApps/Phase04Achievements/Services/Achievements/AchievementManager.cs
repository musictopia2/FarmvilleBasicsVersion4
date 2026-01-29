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
            return false;
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
            return achievement.Target.Value == currentCount ? achievement.CoinReward.Value : 0;
        }

        // REPEATABLE
        if (achievement.RepeatAchievementRules is null || achievement.RepeatRewardRules is null)
        {
            throw new CustomBasicException("Repeatable achievement must have repeat rules and reward rules");
        }

        var targets = achievement.RepeatAchievementRules.FirstTargets;
        var rewards = achievement.RepeatRewardRules.FirstCoinRewards;

        if (targets.Count == 0)
        {
            throw new CustomBasicException("Repeatable achievement must have at least one FirstTarget");
        }
        if (targets.Count != rewards.Count)
        {
            throw new CustomBasicException("Repeat targets and rewards must have same count");
        }

        // 1) Exact match against the explicit tiers
        for (int i = 0; i < targets.Count; i++)
        {
            if (currentCount == targets[i])
            {
                return rewards[i];
            }
        }

        // 2) Exact match against repeating tiers AFTER the last explicit one
        int lastTarget = targets[^1];
        if (currentCount <= lastTarget)
        {
            return 0; // (not an exact match for any first tier, and not beyond last tier)
        }

        int increment = achievement.RepeatAchievementRules.IncrementAfterFirst;
        if (increment <= 0)
        {
            throw new CustomBasicException("IncrementAfterFirst must be > 0");
        }

        int delta = currentCount - lastTarget;

        // must land EXACTLY on a tier
        if (delta % increment != 0)
        {
            return 0;
        }

        // Fixed reward for all tiers after the first set
        int fixedReward = achievement.RepeatRewardRules.CoinRewardAfterFirst
            ?? rewards[^1];

        return fixedReward;
    }
    private static string GetPrimaryText(AchievementPlanModel plan)
    {
        return plan.CounterKey switch
        {
            AchievementCounterKeys.Level => "Reach level",
            AchievementCounterKeys.CompleteScenarios => "Complete scenarios",
            AchievementCounterKeys.CompleteOrders when
                string.IsNullOrWhiteSpace(plan.ItemKey) == false
                    => $"Complete {plan.ItemKey.GetWords} orders",

            AchievementCounterKeys.CompleteOrders
                    => "Complete orders",
            AchievementCounterKeys.SpendCoin => "Spend coins",
            AchievementCounterKeys.CoinEarned => "Earn coins",
            AchievementCounterKeys.UseTimedBoost when
                string.IsNullOrWhiteSpace(plan.OutputAugmentationKey) == false
                    => $"Use {plan.OutputAugmentationKey.GetWords} Power Pin",
            AchievementCounterKeys.UseTimedBoost => $"Use timed boost {plan.SourceKey.GetWords}",
            AchievementCounterKeys.UseConsumable => $"Use {plan.ItemKey.GetWords}",
            AchievementCounterKeys.CollectFromAnimal => $"Collect from {plan.SourceKey.GetWords}",
            AchievementCounterKeys.CraftFromWorkshops => $"Craft {plan.ItemKey.GetWords} from {plan.SourceKey.GetWords}",
            AchievementCounterKeys.FindFromWorksites => $"Find {plan.ItemKey.GetWords} from {plan.SourceKey.GetWords}",
            _ => plan.CounterKey.GetWords
        };
    }
}