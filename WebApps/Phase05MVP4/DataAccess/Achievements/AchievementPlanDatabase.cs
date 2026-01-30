namespace Phase05MVP4.DataAccess.Achievements;
public class AchievementPlanDatabase() : ListDataAccess<AchievementPlanDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration, IAchievementPlanProvider

{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "AchievementPlan";
    async Task<BasicList<AchievementPlanModel>> IAchievementPlanProvider.GetPlanAsync(FarmKey farm)
    {
        var list = await GetDocumentsAsync();
        return list.GetSingleDocument(farm).AchievementPlans;
    }
}