namespace Phase05MVP4.Services.Upgrades;
public interface IWorkshopCapacityUpgradePlanProvider
{
    Task<BasicList<WorkshopCapacityUpgradePlanModel>> GetPlansAsync(FarmKey farm);
}