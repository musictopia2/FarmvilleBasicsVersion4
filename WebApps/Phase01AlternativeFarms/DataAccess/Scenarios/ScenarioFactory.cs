
using Phase01AlternativeFarms.RandomScenarioGeneratorProcesses;

namespace Phase01AlternativeFarms.DataAccess.Scenarios;

public class ScenarioFactory : IScenarioFactory
{


    ScenarioServicesContext IScenarioFactory.GetScenarioServices(FarmKey farm, InstantUnlimitedManager instantUnlimitedManager)
    {
        return new()
        {
            ScenarioGeneration = new RandomScenarioGenerationService(instantUnlimitedManager),
            ScenarioProfile = new ScenarioProfileDatabase(farm)
        };
    }
}