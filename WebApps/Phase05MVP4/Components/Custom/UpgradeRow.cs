namespace Phase05MVP4.Components.Custom;
public sealed record UpgradeColumn(
    int LevelDesired, //starts with 2.
    int LevelRequired,
    Dictionary<string, int> Costs

);