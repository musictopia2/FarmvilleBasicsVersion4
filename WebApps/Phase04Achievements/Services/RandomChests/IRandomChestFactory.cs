namespace Phase04Achievements.Services.RandomChests;
public interface IRandomChestFactory
{
    RandomChestServicesContext GetRandomChestServices(FarmKey farm, ProgressionManager progressionManager);
}
