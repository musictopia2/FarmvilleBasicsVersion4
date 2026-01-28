namespace Phase03RandomChests.Services.RandomChests;
public interface IRandomChestFactory
{
    RandomChestServicesContext GetRandomChestServices(FarmKey farm, ProgressionManager progressionManager);
}
