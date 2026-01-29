namespace Phase21Achievements.DataAccess;
public class AchievementPlanDatabase() : ListDataAccess<AchievementPlanDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration

{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "AchievementPlan";
    public async Task ImportAsync(BasicList<AchievementPlanDocument> list)
    {
        await UpsertRecordsAsync(list);
    }
    public async Task<BasicList<AchievementPlanModel>> GetPlanAsync(FarmKey farm)
    {
        var list = await GetDocumentsAsync();
        return list.GetSingleDocument(farm).AchievementPlans;
    }
}