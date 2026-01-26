namespace Phase02AdvancedUpgrades.Services.Workers;
public interface IWorkerRegistry
{
    Task<BasicList<WorkerRecipe>> GetWorkersAsync();
}