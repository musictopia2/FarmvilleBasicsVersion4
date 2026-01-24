using Phase01AlternativeFarms.Services.Core;

namespace Phase01AlternativeFarms.Services.Balance;
public interface IBaseBalanceProvider
{
    Task<BaseBalanceProfile> GetBaseBalanceAsync(FarmKey farm);
}