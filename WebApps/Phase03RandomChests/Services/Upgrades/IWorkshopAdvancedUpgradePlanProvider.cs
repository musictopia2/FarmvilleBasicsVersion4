namespace Phase03RandomChests.Services.Upgrades;
public interface IWorkshopAdvancedUpgradePlanProvider
{
    Task<BasicList<WorkshopAdvancedUpgradeRuleModel>> GetPlansAsync(FarmKey farm);
}