namespace Phase04Achievements.Components.Custom;
public partial class TreeProgressModal
{
    [Parameter]
    [EditorRequired]
    public TreeView Tree { get; set; }
    [Parameter] public EventCallback OnOpenUpgrades { get; set; }
    private string ReadyText => $"Ready In {TreeManager.TimeLeftForResult(Tree)}";
    private string ProduceText => $"{TreeManager.GetProduceAmount(Tree)} {TreeManager.GetTreeName(Tree)}";
    private bool _advancedUpgrades;
    private int _currentLevel = 0;
    protected override void OnParametersSet()
    {
        _advancedUpgrades = UpgradeManager.HasAdvancedUpgrades;
        if (_advancedUpgrades)
        {
            _currentLevel = TreeManager.GetLevel(Tree);
        }
        base.OnParametersSet();
    }
}