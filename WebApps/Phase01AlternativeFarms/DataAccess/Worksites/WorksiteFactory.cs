using Phase01AlternativeFarms.Services.Core;

namespace Phase01AlternativeFarms.DataAccess.Worksites;
public class WorksiteFactory : IWorksiteFactory
{
    WorksiteServicesContext IWorksiteFactory.GetWorksiteServices(FarmKey farm)
    {
        IWorksiteCollectionPolicy collection;
        collection = new WorksiteManualCollectionPolicy();
        
        WorksiteInstanceDatabase instance = new(farm);

        IWorksiteRegistry register;
        register = new WorksiteRecipeDatabase(farm);
        WorksiteServicesContext output = new()
        {
            WorksiteCollectionPolicy = collection,
            WorksiteRegistry = register,
            WorksiteRepository = instance
        };
        return output;
    }   
}