using Phase05MVP4.Services.Core;

namespace Phase05MVP4.Services.Balance;
public interface IBaseBalanceProvider
{
    Task<BaseBalanceProfile> GetBaseBalanceAsync(FarmKey farm);
}