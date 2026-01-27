namespace Phase03RandomChests.DataAccess.Upgrades;
public class AdvancedUpgradePlanDatabase() : ListDataAccess<AdvancedUpgradePlanDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration, IAdvancedUpgradePlanProvider
{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "AdvancedUpgradePlan";
    async Task<BasicList<AdvancedUpgradePlanModel>> IAdvancedUpgradePlanProvider.GetPlansAsync(FarmKey farm)
    {
        var list = await GetDocumentsAsync();
        return list.GetSingleDocument(farm).Upgrades;
    }
}