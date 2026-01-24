namespace Phase01AlternativeFarms.DataAccess.OutputAugmentation;
public class OutputAugmentationFactory : IOutputAugmentationFactory
{
    OutputAugmentationServicesContext IOutputAugmentationFactory.GetOutputAugmentationServices(FarmKey farm)
    {
        return new()
        {
            OutputAugmentationPlanProvider = new OutputAugmentationPlanDatabase()
        };
    }
}