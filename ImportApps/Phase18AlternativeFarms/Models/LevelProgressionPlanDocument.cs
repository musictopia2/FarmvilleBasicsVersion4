namespace Phase18AlternativeFarms.Models;
public class LevelProgressionPlanDocument : IFarmDocument
{
    required public FarmKey Farm { get; init; }

    /// <summary>
    /// If true: levels continue forever. When tiers run out, the last tier repeats.
    /// </summary>
    public bool IsEndless { get; set; }

    /// <summary>
    /// Ordered list of level tiers (Level 1 uses Tiers[0], Level 2 uses Tiers[1], etc.).
    /// </summary>
    public BasicList<LevelProgressionTier> Tiers { get; init; } = [];
}