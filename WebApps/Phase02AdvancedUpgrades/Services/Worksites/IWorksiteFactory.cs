using Phase02AdvancedUpgrades.Services.Core;

namespace Phase02AdvancedUpgrades.Services.Worksites;
public interface IWorksiteFactory
{
    WorksiteServicesContext GetWorksiteServices(FarmKey farm);
}