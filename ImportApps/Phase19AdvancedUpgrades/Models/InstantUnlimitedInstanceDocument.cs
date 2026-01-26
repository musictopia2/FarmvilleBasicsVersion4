namespace Phase19AdvancedUpgrades.Models;
public class InstantUnlimitedInstanceDocument : IFarmDocumentModel
{
    public required FarmKey Farm { get; set; }
    public BasicList<UnlockModel> Items { get; set; } = [];
}