using Phase05MVP4.Services.Core;

namespace Phase05MVP4.Services.Workshops;
public interface IWorkshopFactory
{
    WorkshopServicesContext GetWorkshopServices(FarmKey farm);
}