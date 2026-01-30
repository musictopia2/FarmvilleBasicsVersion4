namespace Phase05MVP4.Components.Custom;
public partial class AnimalsComponent : IDisposable
{
    private BasicList<AnimalView> _animals = [];
    private TimeSpan? _unlimitedSpeedSeedTime;
    private bool _upgradeMode;
    private bool _anyAdvancedUpgrades;
    private void ToggleUpgradeMode()
    {
        _upgradeMode = !_upgradeMode;
    }
    override protected void OnInitialized()
    {
        AnimalManager.OnAnimalsUpdated += Refresh;
        _unlimitedSpeedSeedTime = TimedBoostManager.GetUnlimitedSpeedSeedTimeLeft();
        TimedBoostManager.Tick += UpdateUnlimitedSpeedSeeds;
        _anyAdvancedUpgrades = UpgradeManager.HasAdvancedUpgrades;
        UpdateAnimals();

    }
    private void Upgraded()
    {
        _upgradeMode = false;
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