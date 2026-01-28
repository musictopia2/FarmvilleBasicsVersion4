using Phase04Achievements.Services.Core;

namespace Phase04Achievements.Services.Workers;
public interface IWorkerFactory
{
    WorkerServicesContext GetWorkerServices(FarmKey farm);
}