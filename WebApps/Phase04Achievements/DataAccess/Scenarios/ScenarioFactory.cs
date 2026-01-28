
using Phase04Achievements.RandomScenarioGeneratorProcesses;

namespace Phase04Achievements.DataAccess.Scenarios;

public class ScenarioFactory : IScenarioFactory
{
    ScenarioServicesContext IScenarioFactory.GetScenarioServices(FarmKey farm, InstantUnlimitedManager instantUnlimitedManager)
    {
        return new()
        {
            ScenarioGeneration = new RandomScenarioGenerationService(instantUnlimitedManager),
            ScenarioProfile = new ScenarioProfileDatabase(farm),
            InventoryStarterRepository = new InventoryStockDatabase()
        };
    }
}