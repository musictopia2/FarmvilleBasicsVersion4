namespace Phase01AlternativeFarms.Services.Scenarios;
public interface IScenarioProfile
{
    Task<ScenarioProfileModel?> LoadAsync();
    Task SaveAsync(ScenarioProfileModel scenario);
}
