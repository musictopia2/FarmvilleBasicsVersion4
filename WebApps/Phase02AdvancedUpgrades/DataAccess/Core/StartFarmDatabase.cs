using Phase02AdvancedUpgrades.Services.Core;

namespace Phase02AdvancedUpgrades.DataAccess.Core;
public class StartFarmDatabase() : ListDataAccess<FarmKey>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration, IStartFarmRegistry

{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "Start";
    async Task<BasicList<FarmKey>> IStartFarmRegistry.GetFarmsAsync()
    {
        var list = await GetDocumentsAsync();
        return list;
    }
}