namespace Phase21Achievements.Models;
public class InventoryStorageUpgradePlanDocument : IFarmDocumentModel, IFarmDocumentFactory<InventoryStorageUpgradePlanDocument>
{
    required public FarmKey Farm { get; init; }
    public BasicList<UpgradeTier> SiloUpgrades { get; init; } = [];
    public BasicList<UpgradeTier> BarnUpgrades { get; init; } = [];

    public static InventoryStorageUpgradePlanDocument CreateEmpty(FarmKey farm)
    {
        return new()
        {
            Farm = farm
        };
    }
}