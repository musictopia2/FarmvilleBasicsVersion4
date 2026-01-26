namespace Phase19AdvancedUpgrades.DataAccess;
public class StartFarmDatabase() : ListDataAccess<FarmKey>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration

{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "Start";
    //hopefully will work.
    //its okay if it wipes out the previos records for this project anyways.
    public async Task ImportAsync(BasicList<FarmKey> list)
    {
        //await UpsertRecordsAsync([]);
        await UpsertRecordsAsync(list);
    }

}