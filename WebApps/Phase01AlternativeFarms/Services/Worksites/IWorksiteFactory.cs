using Phase01AlternativeFarms.Services.Core;

namespace Phase01AlternativeFarms.Services.Worksites;
public interface IWorksiteFactory
{
    WorksiteServicesContext GetWorksiteServices(FarmKey farm);
}