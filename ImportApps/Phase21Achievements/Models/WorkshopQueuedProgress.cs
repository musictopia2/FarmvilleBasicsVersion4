namespace Phase21Achievements.Models;
public class WorkshopQueuedProgress
{
    public required string BuildingName { get; set; }
    public required string ItemCrafted { get; set; }
    public int Count { get; set; }
}