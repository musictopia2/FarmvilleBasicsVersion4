namespace Phase02AdvancedUpgrades.Services.Workshops;
public interface IWorkshopRegistry
{
    Task<BasicList<WorkshopRecipe>> GetWorkshopRecipesAsync();
}