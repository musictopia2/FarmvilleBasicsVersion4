using Phase01AlternativeFarms.Services.Core;

namespace Phase01AlternativeFarms.Services.Trees;
public interface ITreeFactory
{
    TreeServicesContext GetTreeServices(FarmKey farm);
}