namespace Phase02AdvancedUpgrades.Services.Core;
public interface IStartFarmRegistry
{
    Task<BasicList<FarmKey>> GetFarmsAsync(); 
}