namespace Phase04Achievements.DataAccess.OutputAugmentation;
public class OutputAugmentationPlanDatabase() : ListDataAccess<OutputAugmentationPlanDocument>
    (DatabaseName, CollectionName, mm1.DatabasePath),
    ISqlDocumentConfiguration, IOutputAugmentationPlanProvider
{
    public static string DatabaseName => mm1.DatabaseName;
    public static string CollectionName => "OutputAugmentationPlan";
    async Task<BasicList<OutputAugmentationPlanModel>> IOutputAugmentationPlanProvider.GetPlanAsync(FarmKey farm)
    {
        var list = await GetDocumentsAsync();
        return list.GetSingleDocument(farm).Items;
    }
}