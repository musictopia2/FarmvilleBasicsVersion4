namespace Phase02AdvancedUpgrades.Services.Upgrades;
public interface IWorkshopAdvancedUpgradePlanProvider
{
    Task<BasicList<WorkshopAdvancedUpgradeRuleModel>> GetPlansAsync(FarmKey farm);
}