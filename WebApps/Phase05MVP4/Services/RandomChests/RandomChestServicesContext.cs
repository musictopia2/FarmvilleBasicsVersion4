namespace Phase05MVP4.Services.RandomChests;
public class RandomChestServicesContext
{
    public required IRandomChestGenerator RandomChestGenerator { get; init; }
}