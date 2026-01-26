namespace Phase02AdvancedUpgrades.Services.Upgrades;
public interface IAdvancedUpgradePlanProvider
{
    Task<BasicList<AdvancedUpgradePlanModel>> GetPlansAsync(FarmKey farm);
}