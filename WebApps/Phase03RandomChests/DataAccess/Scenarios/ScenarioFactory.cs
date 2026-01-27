
using Phase03RandomChests.RandomScenarioGeneratorProcesses;

namespace Phase03RandomChests.DataAccess.Scenarios;

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