using Phase05MVP4.Services.Core;

namespace Phase05MVP4.Services.Workers;
public interface IWorkerFactory
{
    WorkerServicesContext GetWorkerServices(FarmKey farm);
}