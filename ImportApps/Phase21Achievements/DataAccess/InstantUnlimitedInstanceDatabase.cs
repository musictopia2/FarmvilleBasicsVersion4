namespace Phase21Achievements.DataAccess;
public class InstantUnlimitedInstanceDatabase() : ListDataAccess<InstantUnlimitedInstanceDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration

{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "InstantUnlimitedInstance";
    public async Task ImportAsync(BasicList<InstantUnlimitedInstanceDocument> list)
    {
        await UpsertRecordsAsync(list);
    }
    public async Task<BasicList<UnlockModel>> GetUnlockedItems(FarmKey farm)
    {
        var list = await GetDocumentsAsync();
        return list.GetSingleDocument(farm).Items.Where(x => x.Unlocked).ToBasicList();
    }
}