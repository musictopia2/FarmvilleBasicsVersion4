namespace Phase21Achievements.ImportClasses;
public static class ImportAchievmentProfileClass
{
    public static async Task ImportAchievementsAsync()
    {
        BasicList<FarmKey> farms = FarmHelperClass.GetAllMainFarms();
        BasicList<AchievementProfileDocument> list = [];
        foreach (var farm in farms)
        {
            list.Add(await ImportProfilesAsync(farm));
        }
        list.AddRange(AchievementProfileDocument.PopulateEmptyForCoins());
        list.AddRange(AchievementProfileDocument.PopulateEmptyForCoop());
        AchievementProfileDatabase db = new();
        await db.ImportAsync(list);
    }

    private static async Task<AchievementProfileDocument> ImportProfilesAsync(FarmKey farm)
    {
        AchievementPlanDatabase db = new();
        var list = await db.GetPlanAsync(farm);
        AchievementProfileModel profile = new();
        list.ForConditionalItems(x => x.CounterKey == AchievementCounterKeys.CraftFromWorkshops, item =>
        {
            profile.WorkshopQueued.Add(new()
            {
                BuildingName = item.SourceKey,
                ItemCrafted = item.ItemKey
            });
        });

        list.ForConditionalItems(x => x.CounterKey == AchievementCounterKeys.FindFromWorksites, item =>
        {
            profile.WorksiteFoundProgress.Add(new()
            {
                Location = item.SourceKey,
                Item = item.ItemKey
            });
        });
        list.ForConditionalItems(x => x.CounterKey == AchievementCounterKeys.UseConsumable, item =>
        {
            profile.Consumables.Add(new()
            {
                Key = item.ItemKey,
            });
        });

        list.ForConditionalItems(x => x.CounterKey == AchievementCounterKeys.CollectFromAnimal, item =>
        {
            profile.AnimalCollectProgress.Add(new()
            {
                Name = item.SourceKey
            });
        });
        list.ForConditionalItems(x => x.CounterKey == AchievementCounterKeys.CompleteOrders && x.ItemKey != "", item =>
        {
            profile.OrderItemProgress.Add(new()
            {
                ItemName = item.ItemKey
            });
        });

        list.ForConditionalItems(x => x.CounterKey == AchievementCounterKeys.UseTimedBoost, item =>
        {
            profile.TimedBoostProgress.Add(new()
            {
                ItemKey = item.ItemKey,
                OutputAugmentationKey = item.OutputAugmentationKey,
                SourceKey = item.SourceKey
            });
        });

        AchievementProfileDocument doc = new()
        {
            Farm = farm,
            Profile = profile
        };
        return doc;
    }
}