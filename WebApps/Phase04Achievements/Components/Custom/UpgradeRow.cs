namespace Phase04Achievements.Components.Custom;
public sealed record UpgradeColumn(
    int LevelDesired, //starts with 2.
    int LevelRequired,
    Dictionary<string, int> Costs

);