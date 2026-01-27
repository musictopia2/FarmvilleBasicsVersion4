using Phase03RandomChests.Services.Core;

namespace Phase03RandomChests.Services.Trees;
public interface ITreeFactory
{
    TreeServicesContext GetTreeServices(FarmKey farm);
}