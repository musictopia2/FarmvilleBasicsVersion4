namespace Phase04Achievements.Services.Scenarios;
public interface IScenarioProfile
{
    Task<ScenarioProfileModel?> LoadAsync();
    Task SaveAsync(ScenarioProfileModel scenario);
}
