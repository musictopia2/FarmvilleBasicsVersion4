using Phase02AdvancedUpgrades.Services.Core;

namespace Phase02AdvancedUpgrades.Services.Workers;
public interface IWorkerFactory
{
    WorkerServicesContext GetWorkerServices(FarmKey farm);
}