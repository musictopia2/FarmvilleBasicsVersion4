namespace Phase02AdvancedUpgrades.Components.Custom;
public partial class TreeProgressModal
{
    [Parameter]
    [EditorRequired]
    public TreeView Tree { get; set; }
    private string ReadyText => $"Ready In {TreeManager.TimeLeftForResult(Tree)}";
    private string ProduceText => $"{TreeManager.GetProduceAmount(Tree)} {TreeManager.GetTreeName(Tree)}";
    private bool _advancedUpgrades;
    private int _currentLevel = 0;

    private bool _showUpgrades;

    private void OpenUpgrades()
    {
        _showUpgrades = true;
    }
    private void Upgraded()
    {
        _showUpgrades = false;
    }

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