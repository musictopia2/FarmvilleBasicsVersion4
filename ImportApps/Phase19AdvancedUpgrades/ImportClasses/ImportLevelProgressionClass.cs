namespace Phase19AdvancedUpgrades.ImportClasses;
public static class ImportLevelProgressionClass
{
    public static async Task ImportProgressionAsync()
    {
        LevelProgressionPlanDatabase db = new();
        BasicList<FarmKey> farms = FarmHelperClass.GetAllBaselineFarms();
        BasicList<LevelProgressionPlanDocument> list = [];
        foreach (FarmKey farm in farms)
        {
            //can make one faster to level up than others.
            LevelProgressionPlanDocument plan = new()
            {
                Farm = farm,
                IsEndless = false //cannot be endless for the baseline farms.
            };

            20.Times(x =>
            {
                //pretend like i get 1 at 10 and 2 at 50 and 1 at 80 for testing.
                LevelProgressionTier tier = new()
                {
                    RequiredPoints = 10,
                    RewardsOnLevelComplete = FarmHelperClass.GetCoinOnlyDictionary(100)
                };
                //ProgressMilestoneReward mile = new()
                //{
                //    Percent = 10,
                //    Rewards = FarmHelperClass.GetCoinOnlyDictionary(1)
                //};
                //tier.ProgressMilestoneRewards.Add(mile);
                //mile = new()
                //{
                //    Percent = 50,
                //    Rewards = FarmHelperClass.GetCoinOnlyDictionary(2)
                //};
                //tier.ProgressMilestoneRewards.Add(mile);
                //mile = new()
                //{
                //    Percent = 80,
                //    Rewards = FarmHelperClass.GetCoinOnlyDictionary(1)
                //};
                //tier.ProgressMilestoneRewards.Add(mile);
                plan.Tiers.Add(tier);
            });
            list.Add(plan);
        }
        list.AddRange(LevelProgressionPlanDocument.PopulateEmptyForCoins()); //for now, will not level up.  later will think about quests for the coin farm.
        await db.ImportAsync(list);
    }
}