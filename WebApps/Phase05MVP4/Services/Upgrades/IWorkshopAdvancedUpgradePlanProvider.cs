namespace Phase05MVP4.Services.Upgrades;
public interface IWorkshopAdvancedUpgradePlanProvider
{
    Task<BasicList<WorkshopAdvancedUpgradeRuleModel>> GetPlansAsync(FarmKey farm);
}