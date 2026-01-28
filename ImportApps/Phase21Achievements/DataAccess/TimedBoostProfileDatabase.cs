namespace Phase21Achievements.DataAccess;
public class TimedBoostProfileDatabase() : ListDataAccess<TimedBoostProfileDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration
{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "TimedBoostProfile";

    public async Task ImportAsync(BasicList<TimedBoostProfileDocument> list)
    {
        await UpsertRecordsAsync(list);
    }

    public async Task<TimedBoostProfileDocument> GetProfileAsync(FarmKey farm)
    {
        var list = await GetDocumentsAsync();
        return list.GetSingleDocument(farm);
    }
}