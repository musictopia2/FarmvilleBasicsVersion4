namespace Phase01AlternativeFarms.DataAccess.Scenarios;
public class ScenarioProfileDocument : IFarmDocumentModel
{
    public required FarmKey Farm { get; init; }
    public ScenarioProfileModel? Scenario { get; set; }
}