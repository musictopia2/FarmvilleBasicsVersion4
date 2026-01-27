namespace Phase03RandomChests.Services.Upgrades;
public interface IAdvancedUpgradePlanProvider
{
    Task<BasicList<AdvancedUpgradePlanModel>> GetPlansAsync(FarmKey farm);
}