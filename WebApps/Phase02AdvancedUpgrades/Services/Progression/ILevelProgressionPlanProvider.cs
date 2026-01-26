namespace Phase02AdvancedUpgrades.Services.Progression;
public interface ILevelProgressionPlanProvider
{
    Task<LevelProgressionPlanModel> GetPlanAsync(FarmKey farm);
}