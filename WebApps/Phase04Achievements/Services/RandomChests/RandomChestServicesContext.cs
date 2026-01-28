namespace Phase04Achievements.Services.RandomChests;
public class RandomChestServicesContext
{
    public required IRandomChestGenerator RandomChestGenerator { get; init; }
}