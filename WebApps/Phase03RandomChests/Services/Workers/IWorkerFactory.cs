using Phase03RandomChests.Services.Core;

namespace Phase03RandomChests.Services.Workers;
public interface IWorkerFactory
{
    WorkerServicesContext GetWorkerServices(FarmKey farm);
}