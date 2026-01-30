using Phase05MVP4.Services.Core;

namespace Phase05MVP4.Services.Trees;
public interface ITreeFactory
{
    TreeServicesContext GetTreeServices(FarmKey farm);
}