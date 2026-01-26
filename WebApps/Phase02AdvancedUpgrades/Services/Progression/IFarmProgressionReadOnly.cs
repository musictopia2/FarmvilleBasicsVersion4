namespace Phase02AdvancedUpgrades.Services.Progression;
public interface IFarmProgressionReadOnly
{
    int CurrentLevel { get; }
    bool CompletedGame { get; }
    event Action? Changed;
}