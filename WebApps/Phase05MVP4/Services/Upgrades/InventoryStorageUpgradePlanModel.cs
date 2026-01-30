namespace Phase05MVP4.Services.Upgrades;
public class InventoryStorageUpgradePlanModel
{
    public BasicList<UpgradeTier> SiloUpgrades { get; init; } = [];
    public BasicList<UpgradeTier> BarnUpgrades { get; init; } = [];
}