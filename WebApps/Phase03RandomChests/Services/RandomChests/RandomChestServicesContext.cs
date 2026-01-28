namespace Phase03RandomChests.Services.RandomChests;
public class RandomChestServicesContext
{
    public required IRandomChestGenerator RandomChestGenerator { get; init; }
}