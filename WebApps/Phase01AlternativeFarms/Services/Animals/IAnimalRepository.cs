namespace Phase01AlternativeFarms.Services.Animals;
public interface IAnimalRepository
{
    Task<BasicList<AnimalAutoResumeModel>> LoadAsync();
    Task SaveAsync(BasicList<AnimalAutoResumeModel> animals);
}