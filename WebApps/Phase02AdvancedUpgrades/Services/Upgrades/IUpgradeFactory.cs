namespace Phase02AdvancedUpgrades.Services.Upgrades;
public interface IUpgradeFactory
{
    UpgradeServicesContext GetUpgradeServices(FarmKey farm);
}