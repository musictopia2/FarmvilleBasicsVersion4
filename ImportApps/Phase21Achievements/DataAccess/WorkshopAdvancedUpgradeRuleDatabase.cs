namespace Phase21Achievements.DataAccess;
public class WorkshopAdvancedUpgradeRuleDatabase() : ListDataAccess<WorkshopAdvancedUpgradeRuleDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration

{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "WorkshopAdvancedUpgradeRule";
    public async Task ImportAsync(BasicList<WorkshopAdvancedUpgradeRuleDocument> list)
    {
        await UpsertRecordsAsync(list);
    }
}