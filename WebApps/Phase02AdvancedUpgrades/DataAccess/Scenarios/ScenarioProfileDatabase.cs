namespace Phase02AdvancedUpgrades.DataAccess.Scenarios;
public class ScenarioProfileDatabase(FarmKey farm) : ListDataAccess<ScenarioProfileDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration, IScenarioProfile
{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "ScenarioProfile";
    async Task<ScenarioProfileModel?> IScenarioProfile.LoadAsync()
    {
        var list = await GetDocumentsAsync();
        return list.GetSingleDocument(farm).Scenario;
    }
    async Task IScenarioProfile.SaveAsync(ScenarioProfileModel scenario)
    {
        var list = await GetDocumentsAsync();
        list.GetSingleDocument(farm).Scenario = scenario;
    }
}