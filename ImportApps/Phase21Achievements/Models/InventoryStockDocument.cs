namespace Phase21Achievements.Models;
public class InventoryStockDocument
{
    required public FarmKey Farm { get; set; }
    public Dictionary<string, int> Baseline { get; set; } = [];
    public Dictionary<string, int> Current { get; set; } = [];
    //public Dictionary<string, int> List { get; set; } = [];
}