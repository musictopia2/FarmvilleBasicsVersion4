using Phase04Achievements.Services.Core;

namespace Phase04Achievements.Services.Trees;
public interface ITreeFactory
{
    TreeServicesContext GetTreeServices(FarmKey farm);
}