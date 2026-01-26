namespace Phase02AdvancedUpgrades.Services.TimedBoosts;
public interface ITimedBoostFactory
{
    TimedBoostServicesContext GetTimedBoostServices(FarmKey farm);
}