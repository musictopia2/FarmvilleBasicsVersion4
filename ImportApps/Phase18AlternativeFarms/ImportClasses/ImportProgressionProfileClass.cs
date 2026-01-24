namespace Phase18AlternativeFarms.ImportClasses;
public static class ImportProgressionProfileClass
{
    public static async Task ImportProgressionAsync()
    {
        var farms = FarmHelperClass.GetAllBaselineFarms();
        ProgressionProfileDatabase db = new();
        BasicList<ProgressionProfileDocument> list = [];
        foreach (var farm in farms)
        {
            list.Add(new()
            { 
                Farm = farm,
                PointsThisLevel = 0,
                Level = 1
            }
            );
        }
        list.AddRange(ProgressionProfileDocument.PopulateEmptyForCoins());
        await db.ImportAsync(list);
    }
}