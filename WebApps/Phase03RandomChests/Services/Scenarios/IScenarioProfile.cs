namespace Phase03RandomChests.Services.Scenarios;
public interface IScenarioProfile
{
    Task<ScenarioProfileModel?> LoadAsync();
    Task SaveAsync(ScenarioProfileModel scenario);
}
