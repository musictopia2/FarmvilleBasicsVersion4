namespace Phase05MVP4.Services.Scenarios;
public interface IScenarioProfile
{
    Task<ScenarioProfileModel?> LoadAsync();
    Task SaveAsync(ScenarioProfileModel scenario);
}
