using Phase05MVP4.Services.Core;

namespace Phase05MVP4.DataAccess.Trees;
public class TreeFactory : ITreeFactory
{
    TreeServicesContext ITreeFactory.GetTreeServices(FarmKey farm)
    {
        ITreeGatheringPolicy collection;
        collection = new TreeGatherAllPolicy();
        ITreeRecipes register;
        register = new TreeRecipeDatabase(farm);
        TreeInstanceDatabase instance = new(farm);
        TreeServicesContext output = new()
        {
            TreeGatheringPolicy = collection,
            TreeRegistry = register,
            TreeRepository = instance,
            TreesCollecting = new DefaultTreesCollected(),
        };
        return output;
    }   
}