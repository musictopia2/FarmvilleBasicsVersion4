namespace Phase18AlternativeFarms.DataAccess;
public class RentalInstanceDatabase()
    : ListDataAccess<RentalInstanceDocument>
        (DatabaseName, CollectionName, mm1.DatabasePath),
      ISqlDocumentConfiguration
{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "RentalInstance";
    public async Task ImportAsync(BasicList<RentalInstanceDocument> list)
    {
        await UpsertRecordsAsync(list);
    }
}