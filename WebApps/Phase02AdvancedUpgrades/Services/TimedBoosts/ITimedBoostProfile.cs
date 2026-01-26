namespace Phase02AdvancedUpgrades.Services.TimedBoosts;
public interface ITimedBoostProfile
{
    Task<TimedBoostProfileModel> LoadAsync();
    Task SaveAsync(TimedBoostProfileModel model);
}