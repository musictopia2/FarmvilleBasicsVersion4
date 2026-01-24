namespace Phase18AlternativeFarms.Models;
public class InventoryStorageUpgradePlanDocument : IFarmDocument, IFarmDocumentFactory<InventoryStorageUpgradePlanDocument>
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