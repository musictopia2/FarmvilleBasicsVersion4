namespace Phase05MVP4.RandomScenarioGeneratorProcesses;
public class RandomScenarioGenerationService(
    InstantUnlimitedManager instantUnlimitedManager
    ) : IScenarioGenerationService
{
    BasicList<ScenarioInstance> IScenarioGenerationService.GetScenarios()
    {
        BasicList<ScenarioInstance> output = [];
        //looks like i may need something else in order to figure out what to do for the scenarios.

        var list = instantUnlimitedManager.GetInstantItems;
        int tasks;
        tasks = rs1.Randoms.GetRandomNumber(30, 25);

        tasks.Times(x =>
        {
            string item = list.GetRandomItem();
            int asks = rs1.Randoms.GetRandomNumber(120, 90);
            output.Add(new()
            {
                ScenarioId = Guid.NewGuid().ToString(),
                Completed = false,
                Tracked = false,
                Item = item,
                Required = asks
            });
        });




        return output;
    }
}
