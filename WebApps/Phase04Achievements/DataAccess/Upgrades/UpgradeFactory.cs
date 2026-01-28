namespace Phase04Achievements.DataAccess.Upgrades;
public class UpgradeFactory : IUpgradeFactory
{
    UpgradeServicesContext IUpgradeFactory.GetUpgradeServices(FarmKey farm)
    {
        return new UpgradeServicesContext()
        {
            InventoryStorageUpgradePlanProvider = new InventoryStorageUpgradePlanDatabase(),
            WorkshopCapacityUpgradePlanProvider = new WorkshopCapacityUpgradePlanDatabase(),
            AdvancedUpgradePlanProvider = new AdvancedUpgradePlanDatabase(),
            WorkshopAdvancedUpgradePlanProvider = new WorkshopAdvancedUpgradeRuleDatabase()
        };
    }
}