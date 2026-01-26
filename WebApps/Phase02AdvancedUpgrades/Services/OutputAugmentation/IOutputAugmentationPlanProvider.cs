namespace Phase02AdvancedUpgrades.Services.OutputAugmentation;
public interface IOutputAugmentationPlanProvider
{
    Task<BasicList<OutputAugmentationPlanModel>> GetPlanAsync(FarmKey farm);
}