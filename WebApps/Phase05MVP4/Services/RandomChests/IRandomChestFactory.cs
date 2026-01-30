namespace Phase05MVP4.Services.RandomChests;
public interface IRandomChestFactory
{
    RandomChestServicesContext GetRandomChestServices(FarmKey farm, ProgressionManager progressionManager);
}
