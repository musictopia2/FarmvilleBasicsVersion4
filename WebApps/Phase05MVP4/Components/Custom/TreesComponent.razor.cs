namespace Phase05MVP4.Components.Custom;
public partial class TreesComponent
{
    private BasicList<TreeView> _trees = [];
    private TimeSpan? _unlimitedSpeedSeedTime;
    protected override void OnInitialized()
    {
        Refresh();
        base.OnInitialized();
    }
    protected override Task OnTickAsync()
    {
        Refresh();
        return base.OnTickAsync();
    }
    private void Refresh()
    {
        _trees = TreeManager.GetUnlockedTrees;
        _unlimitedSpeedSeedTime = TimedBoostManager.GetUnlimitedSpeedSeedTimeLeft();
    }
}