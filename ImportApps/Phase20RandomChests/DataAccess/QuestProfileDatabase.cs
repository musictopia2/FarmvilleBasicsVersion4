namespace Phase20RandomChests.DataAccess;

public class QuestProfileDatabase() : ListDataAccess<QuestProfileDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration

{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "QuestProfile";
    public async Task ImportAsync(BasicList<QuestProfileDocument> list)
    {
        await UpsertRecordsAsync(list);
    }

}