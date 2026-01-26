namespace Phase02AdvancedUpgrades.Services.OutputAugmentation;
public interface IOutputAugmentationFactory
{
    OutputAugmentationServicesContext GetOutputAugmentationServices(FarmKey farm);
}