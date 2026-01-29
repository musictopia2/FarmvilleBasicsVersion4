namespace Phase04Achievements.DataAccess.Achievements;
public class AchievementProfileDatabase(FarmKey farm) : ListDataAccess<AchievementProfileDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration, IAchievementProfile

{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "AchievementProfile";

    async Task<AchievementProfileModel> IAchievementProfile.LoadAsync()
    {
        var list = await GetDocumentsAsync();
        return list.GetSingleDocument(farm).Profile;
    }

    async Task IAchievementProfile.SaveAsync(AchievementProfileModel profile)
    {
        var list = await GetDocumentsAsync();
        list.GetSingleDocument(farm).Profile = profile;
        await UpsertRecordsAsync(list);
    }
}