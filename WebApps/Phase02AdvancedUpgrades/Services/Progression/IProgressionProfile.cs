namespace Phase02AdvancedUpgrades.Services.Progression;
public interface IProgressionProfile
{
    Task<ProgressionProfileModel> LoadAsync();
    Task SaveAsync(ProgressionProfileModel profile);
}