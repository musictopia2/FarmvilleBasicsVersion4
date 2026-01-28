namespace Phase04Achievements.DataAccess.Upgrades;
public class WorkshopAdvancedUpgradeRuleDatabase() : ListDataAccess<WorkshopAdvancedUpgradeRuleDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration, IWorkshopAdvancedUpgradePlanProvider

{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "WorkshopAdvancedUpgradeRule";
    async Task<BasicList<WorkshopAdvancedUpgradeRuleModel>> IWorkshopAdvancedUpgradePlanProvider.GetPlansAsync(FarmKey farm)
    {
        var list = await GetDocumentsAsync();

        BasicList<WorkshopAdvancedUpgradeRuleModel> output = [];

        list.ForConditionalItems(x => x.Theme == farm.Theme, item =>
        {
            output.Add(new()
            {
                BuildingName = item.BuildingName,
                TierLevelRequired = item.TierLevelRequired
            });
        });
        return output;
    }
}