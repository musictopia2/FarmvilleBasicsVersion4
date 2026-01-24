namespace Phase01AlternativeFarms.DataAccess.Rentals;
public class RentalInstanceDatabase(FarmKey farm)
    : ListDataAccess<RentalInstanceDocument>
        (DatabaseName, CollectionName, mm1.DatabasePath),
      ISqlDocumentConfiguration, IRentalProfile
{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "RentalInstance";
    async Task<BasicList<RentalInstanceModel>> IRentalProfile.LoadAsync()
    {
        var list = await GetDocumentsAsync();
        return list.GetSingleDocument(farm).Rentals;
    }
    async Task IRentalProfile.SaveAsync(BasicList<RentalInstanceModel> rentals)
    {
        var list = await GetDocumentsAsync();
        list.GetSingleDocument(farm).Rentals = rentals;
        await UpsertRecordsAsync(list);
    }
}