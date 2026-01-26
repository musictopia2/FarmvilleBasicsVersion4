namespace Phase02AdvancedUpgrades.Services.InstantUnlimited;
public interface IInstantUnlimitedFactory
{
    InstantUnlimitedServicesContext GetInstantUnlimitedServices(FarmKey farm);
}