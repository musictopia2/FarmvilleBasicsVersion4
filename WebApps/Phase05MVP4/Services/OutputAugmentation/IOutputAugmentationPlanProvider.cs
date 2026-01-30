namespace Phase05MVP4.Services.OutputAugmentation;
public interface IOutputAugmentationPlanProvider
{
    Task<BasicList<OutputAugmentationPlanModel>> GetPlanAsync(FarmKey farm);
}