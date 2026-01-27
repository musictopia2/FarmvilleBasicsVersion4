namespace Phase03RandomChests.Services.Worksites;
public class WorksiteManualCollectionPolicy : IWorksiteCollectionPolicy
{
    Task<bool> IWorksiteCollectionPolicy.CollectAllAsync()
    {
        return Task.FromResult(false);
    }

}