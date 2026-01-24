namespace Phase01AlternativeFarms.Services.OutputAugmentation;
public interface IOutputAugmentationFactory
{
    OutputAugmentationServicesContext GetOutputAugmentationServices(FarmKey farm);
}