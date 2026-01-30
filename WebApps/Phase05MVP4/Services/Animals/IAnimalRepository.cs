namespace Phase05MVP4.Services.Animals;
public interface IAnimalRepository
{
    Task<BasicList<AnimalAutoResumeModel>> LoadAsync();
    Task SaveAsync(BasicList<AnimalAutoResumeModel> animals);
}