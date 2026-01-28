namespace Phase04Achievements.Services.OutputAugmentation;
public interface IOutputAugmentationFactory
{
    OutputAugmentationServicesContext GetOutputAugmentationServices(FarmKey farm);
}