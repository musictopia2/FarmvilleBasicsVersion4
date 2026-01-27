namespace Phase20RandomChests.DataAccess;
internal class WorkerRecipeDatabase() : ListDataAccess<WorkerRecipeDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration

{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "WorkerRecipes";
    public async Task ImportAsync(BasicList<WorkerRecipeDocument> list)
    {
        await UpsertRecordsAsync(list);
    }
    public async Task<BasicList<WorkerRecipeDocument>> GetWorkersAsync(string theme)
    {
        var list = await GetDocumentsAsync();
        return list.Where(x => x.Theme == theme).ToBasicList();
    }
}