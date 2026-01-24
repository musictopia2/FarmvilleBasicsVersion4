namespace Phase01AlternativeFarms.Services.Workers;
public interface IWorkerRepository
{
    Task<BasicList<UnlockModel>> LoadAsync();
    Task SaveAsync(BasicList<UnlockModel> data);
}