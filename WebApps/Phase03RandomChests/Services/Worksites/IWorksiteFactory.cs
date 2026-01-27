using Phase03RandomChests.Services.Core;

namespace Phase03RandomChests.Services.Worksites;
public interface IWorksiteFactory
{
    WorksiteServicesContext GetWorksiteServices(FarmKey farm);
}