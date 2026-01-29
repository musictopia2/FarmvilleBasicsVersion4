namespace Phase21Achievements.ImportClasses;
internal static class ImportScenariosClass
{
    public static async Task ImportScenariosAsync()
    {
        var farms = FarmHelperClass.GetAllBaselineFarms();
        BasicList<ScenarioProfileDocument> list = [];
        foreach (var farm in farms)
        {
            ScenarioProfileDocument doc = new()
            {
                Farm = farm
            };
            list.Add(doc);
        }
        farms = FarmHelperClass.GetAllCoinFarms();
        foreach (var farm in farms)
        {
            ScenarioProfileModel scenario = new()
            {
                TimeBetween = TimeSpan.FromSeconds(10),
                Status = EnumScenarioStatus.None,
                Tasks = [],
                LastCompleted = null,
                Rewards = 1000 //for testing, you receive 1000 coins.  that can change.
            };
            list.Add(new()
            {
                Farm = farm,
                Scenario = scenario
            });
        }
        ScenarioProfileDatabase db = new();
        await db.ImportAsync(list);
    }
}
