namespace Phase02AdvancedUpgrades.Services.Worksites;
public interface IWorksiteRegistry
{
    Task<BasicList<WorksiteRecipe>> GetWorksitesAsync();
}