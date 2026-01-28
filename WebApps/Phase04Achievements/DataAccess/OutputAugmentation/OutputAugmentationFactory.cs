namespace Phase04Achievements.DataAccess.OutputAugmentation;
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