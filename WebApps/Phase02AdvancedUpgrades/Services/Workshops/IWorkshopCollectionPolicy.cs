namespace Phase02AdvancedUpgrades.Services.Workshops;
public interface IWorkshopCollectionPolicy
{
    Task<bool> IsAutomaticAsync();
}