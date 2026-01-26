namespace Phase19AdvancedUpgrades.DataAccess;
public class WorkshopAdvancedUpgradeRuleDatabase() : ListDataAccess<WorkshopAdvancedUpgradeRuleDatabase>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration

{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "WorkshopAdvancedUpgradeRule";
    public async Task ImportAsync(BasicList<WorkshopAdvancedUpgradeRuleDatabase> list)
    {
        await UpsertRecordsAsync(list);
    }
}