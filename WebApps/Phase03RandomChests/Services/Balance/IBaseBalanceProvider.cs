using Phase03RandomChests.Services.Core;

namespace Phase03RandomChests.Services.Balance;
public interface IBaseBalanceProvider
{
    Task<BaseBalanceProfile> GetBaseBalanceAsync(FarmKey farm);
}