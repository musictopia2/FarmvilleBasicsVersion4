namespace Phase02AdvancedUpgrades.Services.Trees;
public interface ITreeRecipes
{
    Task<BasicList<TreeRecipe>> GetTreesAsync();
}