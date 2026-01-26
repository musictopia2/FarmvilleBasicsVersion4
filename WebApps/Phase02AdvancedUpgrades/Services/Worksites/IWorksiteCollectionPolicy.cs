namespace Phase02AdvancedUpgrades.Services.Worksites;
public interface IWorksiteCollectionPolicy
{
    Task<bool> CollectAllAsync();
}