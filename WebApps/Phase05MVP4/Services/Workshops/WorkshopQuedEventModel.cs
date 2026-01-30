namespace Phase05MVP4.Services.Workshops;
public class WorkshopQuedEventModel
{
    public required string BuildingName { get; init; } // "windmill"
    public required string ItemCrafted { get; init; }  // "wheat" (the recipe output item key)
}