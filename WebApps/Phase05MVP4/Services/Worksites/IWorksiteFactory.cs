using Phase05MVP4.Services.Core;

namespace Phase05MVP4.Services.Worksites;
public interface IWorksiteFactory
{
    WorksiteServicesContext GetWorksiteServices(FarmKey farm);
}