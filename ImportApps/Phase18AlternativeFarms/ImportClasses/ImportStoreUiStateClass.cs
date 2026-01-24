namespace Phase18AlternativeFarms.ImportClasses;
public static class ImportStoreUiStateClass
{
    public static async Task ImportUiStoreStateAsync()
    {
        BasicList<FarmKey> farms = FarmHelperClass.GetAllCompleteFarms();
        BasicList<StoreUiStateDocument> list = [];
        foreach (var item in farms)
        {
            StoreUiStateDocument document = new()
            {
                Farm = item,
            };
            list.Add(document);
        }
        StoreUiStateDatabase db = new();
        await db.ImportAsync(list);
    }
}