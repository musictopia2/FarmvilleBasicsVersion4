namespace Phase21Achievements.ImportClasses;
public static class ImportStartClass
{
    public static async Task ImportStartAsync()
    {
        StartFarmDatabase db = new();
        await db.ImportAsync(FarmHelperClass.GetAllCompleteFarms()); //clearly needs all so you have all farms.
    }
}