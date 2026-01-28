namespace Phase04Achievements.DataAccess.RandomChests;
public class RandomChestPlanDatabase(FarmKey farm) : ListDataAccess<RandomChestPlanDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration

{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "RandomChestPlan";
    public async Task<RandomChestPlanDocument> GetPlanAsync()
    {
        var list = await GetDocumentsAsync();
        return list.GetSingleDocument(farm);
    }
}