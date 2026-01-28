namespace Phase20RandomChests.DataAccess;
public class RandomChestPlanDatabase() : ListDataAccess<RandomChestPlanDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration

{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "RandomChestPlan";
    public async Task ImportAsync(BasicList<RandomChestPlanDocument> list)
    {
        await UpsertRecordsAsync(list);
    }
}