using Phase04Achievements.Services.Core;

namespace Phase04Achievements.Services.Balance;
public interface IBaseBalanceProvider
{
    Task<BaseBalanceProfile> GetBaseBalanceAsync(FarmKey farm);
}