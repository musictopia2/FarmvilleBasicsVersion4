namespace Phase02AdvancedUpgrades.Services.Workshops;
public class WorkshopAutomatedCollectionPolicy : IWorkshopCollectionPolicy
{
    Task<bool> IWorkshopCollectionPolicy.IsAutomaticAsync()
    {
        return Task.FromResult(true);
    }
}