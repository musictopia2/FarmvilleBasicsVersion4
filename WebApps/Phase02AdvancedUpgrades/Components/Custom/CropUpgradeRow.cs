namespace Phase02AdvancedUpgrades.Components.Custom;
public sealed record CropUpgradeColumn(
    int LevelDesired, //starts with 2.
    int LevelRequired,
    Dictionary<string, int> Costs

);