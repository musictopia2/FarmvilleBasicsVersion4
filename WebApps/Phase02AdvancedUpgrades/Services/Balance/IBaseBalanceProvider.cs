using Phase02AdvancedUpgrades.Services.Core;

namespace Phase02AdvancedUpgrades.Services.Balance;
public interface IBaseBalanceProvider
{
    Task<BaseBalanceProfile> GetBaseBalanceAsync(FarmKey farm);
}