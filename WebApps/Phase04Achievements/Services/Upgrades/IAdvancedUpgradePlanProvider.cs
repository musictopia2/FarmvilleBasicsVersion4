namespace Phase04Achievements.Services.Upgrades;
public interface IAdvancedUpgradePlanProvider
{
    Task<BasicList<AdvancedUpgradePlanModel>> GetPlansAsync(FarmKey farm);
}