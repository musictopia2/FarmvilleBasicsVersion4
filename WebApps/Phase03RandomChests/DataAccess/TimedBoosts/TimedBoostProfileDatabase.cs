namespace Phase03RandomChests.DataAccess.TimedBoosts;
public class TimedBoostProfileDatabase(FarmKey farm) : ListDataAccess<TimedBoostProfileDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration, ITimedBoostProfile
{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "TimedBoostProfile";
    async Task<TimedBoostProfileModel> ITimedBoostProfile.LoadAsync()
    {
        var list = await GetDocumentsAsync();
        var item = list.GetSingleDocument(farm);
        return new()
        {
            Active = item.Active,
            Credits = item.Credits,
        };
    }
    async Task ITimedBoostProfile.SaveAsync(TimedBoostProfileModel model)
    {
        var list = await GetDocumentsAsync();
        var item = list.GetSingleDocument(farm);
        item.Active = model.Active;
        item.Credits = model.Credits;
        await UpsertRecordsAsync(list);
    }
}