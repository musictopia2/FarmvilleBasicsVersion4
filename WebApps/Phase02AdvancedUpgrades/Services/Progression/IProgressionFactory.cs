namespace Phase02AdvancedUpgrades.Services.Progression;
public interface IProgressionFactory
{
    ProgressionServicesContext GetProgressionServices(FarmKey farm);
}