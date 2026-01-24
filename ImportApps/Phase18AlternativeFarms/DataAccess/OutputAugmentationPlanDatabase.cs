namespace Phase18AlternativeFarms.DataAccess;
public class OutputAugmentationPlanDatabase() : ListDataAccess<OutputAugmentationPlanDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration
{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "OutputAugmentationPlan";
    public async Task ImportAsync(BasicList<OutputAugmentationPlanDocument> list)
    {
        await UpsertRecordsAsync(list);
    }
}