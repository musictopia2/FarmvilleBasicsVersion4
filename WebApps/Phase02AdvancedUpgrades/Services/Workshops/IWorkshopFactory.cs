using Phase02AdvancedUpgrades.Services.Core;

namespace Phase02AdvancedUpgrades.Services.Workshops;
public interface IWorkshopFactory
{
    WorkshopServicesContext GetWorkshopServices(FarmKey farm);
}