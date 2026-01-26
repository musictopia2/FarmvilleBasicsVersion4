namespace Phase02AdvancedUpgrades.DataAccess.Inventory;
public class InventoryStockDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    public Dictionary<string, int> Baseline { get; set; } = [];
    public Dictionary<string, int> Current { get; set; } = [];
}