namespace Phase04Achievements.Components.Custom;
public abstract class FarmComponentBase : ComponentBase
{
    [CascadingParameter]
    protected MainFarmContainer? Farm { get; set; } //not common enough for the instant unlimited.  just do farm then what is needed this time.
    protected CropManager CropManager => Farm!.CropManager;
    protected TreeManager TreeManager => Farm!.TreeManager;
    protected WorkshopManager WorkshopManager => Farm!.WorkshopManager;
    protected AnimalManager AnimalManager => Farm!.AnimalManager;
    public WorksiteManager WorksiteManager => Farm!.WorksiteManager;
    protected InventoryManager InventoryManager => Farm!.InventoryManager;
    protected UpgradeManager UpgradeManager => Farm!.UpgradeManager;
    protected ProgressionManager ProgressionManager => Farm!.ProgressionManager;
    protected StoreManager StoreManager => Farm!.StoreManager;
    protected TimedBoostManager TimedBoostManager => Farm!.TimedBoostManager;
    protected ItemManager ItemManager => Farm!.ItemManager;
    protected RentalManager RentalManager => Farm!.RentalManager;
    protected ScenarioManager ScenarioManager => Farm!.ScenarioManager;
    public FarmKey Key => Farm!.FarmKey;
}