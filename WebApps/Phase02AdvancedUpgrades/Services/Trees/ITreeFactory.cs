using Phase02AdvancedUpgrades.Services.Core;

namespace Phase02AdvancedUpgrades.Services.Trees;
public interface ITreeFactory
{
    TreeServicesContext GetTreeServices(FarmKey farm);
}