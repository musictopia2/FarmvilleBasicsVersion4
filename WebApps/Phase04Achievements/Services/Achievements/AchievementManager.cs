namespace Phase04Achievements.Services.Achievements;
public class AchievementManager(InventoryManager inventoryManager)
{
    private BasicList<AchievementPlanModel> _plans = [];

    public async Task SetAchievementStyleContextAsync(AchievementServicesContext context, FarmKey farm)
    {
        _plans = await context.AchievementPlanProvider.GetPlanAsync(farm);



    }


}