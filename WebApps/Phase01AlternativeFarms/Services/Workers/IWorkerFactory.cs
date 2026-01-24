using Phase01AlternativeFarms.Services.Core;

namespace Phase01AlternativeFarms.Services.Workers;
public interface IWorkerFactory
{
    WorkerServicesContext GetWorkerServices(FarmKey farm);
}