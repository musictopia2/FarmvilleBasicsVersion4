
namespace Phase05MVP4.Services.Trees;
public class TreeGatherAllPolicy : ITreeGatheringPolicy
{
    Task<bool> ITreeGatheringPolicy.CollectAllAsync()
    {
        return Task.FromResult(true);
    }
}