namespace Phase19AdvancedUpgrades.DataAccess;
public class ScenarioProfileDatabase() : ListDataAccess<ScenarioProfileDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration
{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "ScenarioProfile";
    public async Task ImportAsync(BasicList<ScenarioProfileDocument> list)
    {
        await UpsertRecordsAsync(list);
    }
}