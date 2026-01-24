namespace Phase01AlternativeFarms.Components.Custom;
public partial class AnimalsComponent : IDisposable
{
    private BasicList<AnimalView> _animals = [];
    private TimeSpan? _unlimitedSpeedSeedTime;
    override protected void OnInitialized()
    {
        AnimalManager.OnAnimalsUpdated += Refresh;
        _unlimitedSpeedSeedTime = TimedBoostManager.GetUnlimitedSpeedSeedTimeLeft();
        TimedBoostManager.Tick += UpdateUnlimitedSpeedSeeds;
        UpdateAnimals();

    }
    private void UpdateAnimals()
    {
        _animals = AnimalManager.GetUnlockedAnimals;
    }
    private void UpdateUnlimitedSpeedSeeds()
    {
        _unlimitedSpeedSeedTime = TimedBoostManager.GetUnlimitedSpeedSeedTimeLeft();
        InvokeAsync(StateHasChanged);
    }

    private void Refresh()
    {
        UpdateAnimals();
        InvokeAsync(StateHasChanged);
    }
    public void Dispose()
    {
        AnimalManager.OnAnimalsUpdated -= Refresh;
        TimedBoostManager.Tick -= UpdateUnlimitedSpeedSeeds;
        GC.SuppressFinalize(this);
    }
}