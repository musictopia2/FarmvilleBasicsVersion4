namespace Phase01AlternativeFarms.DataAccess.Upgrades;
public class UpgradeFactory : IUpgradeFactory
{
    UpgradeServicesContext IUpgradeFactory.GetUpgradeServices(FarmKey farm)
    {
        return new UpgradeServicesContext()
        {
            InventoryStorageUpgradePlanProvider = new InventoryStorageUpgradePlanDatabase(),
            WorkshopCapacityUpgradePlanProvider = new WorkshopCapacityUpgradePlanDatabase(),
        };
    }
}