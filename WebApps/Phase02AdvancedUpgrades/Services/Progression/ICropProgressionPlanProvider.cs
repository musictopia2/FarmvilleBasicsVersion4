namespace Phase02AdvancedUpgrades.Services.Progression;
public interface ICropProgressionPlanProvider
{
    Task<CropProgressionPlanModel> GetPlanAsync(FarmKey farm);
}