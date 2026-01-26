namespace Phase19AdvancedUpgrades.Models;
public class ScenarioProfileDocument : IFarmDocumentModel
{
    public required FarmKey Farm { get; init; }
    public ScenarioProfileModel? Scenario { get; set; }
}