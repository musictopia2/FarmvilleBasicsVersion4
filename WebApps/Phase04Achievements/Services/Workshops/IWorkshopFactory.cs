using Phase04Achievements.Services.Core;

namespace Phase04Achievements.Services.Workshops;
public interface IWorkshopFactory
{
    WorkshopServicesContext GetWorkshopServices(FarmKey farm);
}