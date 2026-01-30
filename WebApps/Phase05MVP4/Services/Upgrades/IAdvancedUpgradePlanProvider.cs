namespace Phase05MVP4.Services.Upgrades;
public interface IAdvancedUpgradePlanProvider
{
    Task<BasicList<AdvancedUpgradePlanModel>> GetPlansAsync(FarmKey farm);
}