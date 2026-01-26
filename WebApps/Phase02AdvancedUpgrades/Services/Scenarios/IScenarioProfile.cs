namespace Phase02AdvancedUpgrades.Services.Scenarios;
public interface IScenarioProfile
{
    Task<ScenarioProfileModel?> LoadAsync();
    Task SaveAsync(ScenarioProfileModel scenario);
}
