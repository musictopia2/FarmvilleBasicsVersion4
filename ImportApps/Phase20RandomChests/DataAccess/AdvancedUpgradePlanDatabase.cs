namespace Phase20RandomChests.DataAccess;
public class AdvancedUpgradePlanDatabase() : ListDataAccess<AdvancedUpgradePlanDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration

{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "AdvancedUpgradePlan";
    public async Task ImportAsync(BasicList<AdvancedUpgradePlanDocument> list)
    {
        await UpsertRecordsAsync(list);
    }
}