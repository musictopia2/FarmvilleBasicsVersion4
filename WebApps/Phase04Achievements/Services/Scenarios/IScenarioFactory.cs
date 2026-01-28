namespace Phase04Achievements.Services.Scenarios;
public interface IScenarioFactory
{
    //the quest one needed the crop and tree manager.  hopefully this won't need it  (not sure yet).
    ScenarioServicesContext GetScenarioServices(FarmKey farm, InstantUnlimitedManager instantUnlimitedManager);
}