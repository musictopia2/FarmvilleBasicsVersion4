using Phase01AlternativeFarms.Services.Core;

namespace Phase01AlternativeFarms.Services.Workshops;
public interface IWorkshopFactory
{
    WorkshopServicesContext GetWorkshopServices(FarmKey farm);
}