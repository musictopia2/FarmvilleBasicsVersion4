namespace Phase02AdvancedUpgrades.DataAccess.InstantUnlimited;
public class InstantUnlimitedInstanceDatabase(FarmKey farm) : ListDataAccess<InstantUnlimitedInstanceDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration, IInstantUnlimitedProfile

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

    async Task<BasicList<UnlockModel>> IInstantUnlimitedProfile.LoadAsync()
    {
        var list = await GetDocumentsAsync();
        return list.GetSingleDocument(farm).Items.ToBasicList();
    }

    async Task IInstantUnlimitedProfile.SaveAsync(BasicList<UnlockModel> list)
    {
        var temps = await GetDocumentsAsync();
        temps.GetSingleDocument(farm).Items = list;
        await UpsertRecordsAsync(temps);
    }
}