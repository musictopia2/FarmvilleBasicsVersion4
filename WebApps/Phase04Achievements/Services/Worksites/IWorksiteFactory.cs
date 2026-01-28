using Phase04Achievements.Services.Core;

namespace Phase04Achievements.Services.Worksites;
public interface IWorksiteFactory
{
    WorksiteServicesContext GetWorksiteServices(FarmKey farm);
}