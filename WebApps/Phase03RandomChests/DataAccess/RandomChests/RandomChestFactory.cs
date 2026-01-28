namespace Phase03RandomChests.DataAccess.RandomChests;
public class RandomChestFactory : IRandomChestFactory
{
    RandomChestServicesContext IRandomChestFactory.GetRandomChestServices(FarmKey farm, ProgressionManager progressionManager)
    {
        return new()
        {
            RandomChestGenerator = new RandomChestRewardGenerator(new RandomChestPlanDatabase(farm), progressionManager)
        };
    }
}