
using Phase05MVP4.RandomScenarioGeneratorProcesses;

namespace Phase05MVP4.DataAccess.Scenarios;

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