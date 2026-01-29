namespace Phase21Achievements.DataAccess;
public class AchievementProfileDatabase() : ListDataAccess<AchievementProfileDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration

{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "AchievementProfile";
    public async Task ImportAsync(BasicList<AchievementProfileDocument> list)
    {
        await UpsertRecordsAsync(list);
    }
}