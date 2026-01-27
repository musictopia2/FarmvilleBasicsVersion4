namespace Phase20RandomChests.Models;
public class WorkshopAdvancedUpgradeRuleDocument
{
    public required string Theme { get; init; }
    public required string BuildingName { get; init; } //must repeat buildings (nothing i can do about it).

    // Absolute player levels required for each tier (same count as your workshop tiers: 5)
    public required BasicList<int> TierLevelRequired { get; init; }
}