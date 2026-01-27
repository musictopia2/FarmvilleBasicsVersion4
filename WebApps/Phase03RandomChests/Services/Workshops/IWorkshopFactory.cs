using Phase03RandomChests.Services.Core;

namespace Phase03RandomChests.Services.Workshops;
public interface IWorkshopFactory
{
    WorkshopServicesContext GetWorkshopServices(FarmKey farm);
}