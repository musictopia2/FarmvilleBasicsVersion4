namespace Phase03RandomChests.Services.Core;
public class MainFarmContainer
{
    required public FarmKey FarmKey { get; set; }
    required public CropManager CropManager { get; set; }
    required public TreeManager TreeManager { get; set; }
    required public InventoryManager InventoryManager { get; set; }
    required public AnimalManager AnimalManager { get; set; }
    required public WorkshopManager WorkshopManager { get; set; }
    required public WorksiteManager WorksiteManager { get; set; }
    required public QuestManager QuestManager { get; set; }
    required public ScenarioManager ScenarioManager { get; set; }
    required public UpgradeManager UpgradeManager { get; set; }
    required public ProgressionManager ProgressionManager { get; set; }
    required public CatalogManager CatalogManager { get; set; }
    required public StoreManager StoreManager { get; set; }

    required public InstantUnlimitedManager InstantUnlimitedManager { get; set; }
    required public TimedBoostManager TimedBoostManager { get; set; }
    required public ItemManager ItemManager { get; set; }
    required public OutputAugmentationManager OutputAugmentationManager { get; set; }
    required public RentalManager RentalManager { get; set; }
    required public RandomChestManager RandomChestManager { get; set; }
    //attempt to not require itemmanager here (since only the quest manager should require it.   if i am wrong, rethink).
}