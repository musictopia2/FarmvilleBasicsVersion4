namespace Phase19AdvancedUpgrades.Models;
public class AnimalProgressionPlanDocument : IFarmDocumentModel //repeat for others for future understanding.
{
    required public FarmKey Farm { get; set; }
    public BasicList<ItemUnlockRule> UnlockRules { get; set; } = [];
}