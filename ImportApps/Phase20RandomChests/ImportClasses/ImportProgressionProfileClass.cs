namespace Phase20RandomChests.ImportClasses;
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
                Level = 34 //so i can test current level with all possibilties
            }
            );
        }
        list.AddRange(ProgressionProfileDocument.PopulateEmptyForCoins());
        await db.ImportAsync(list);
    }
}