namespace Phase05MVP4.Services.Trees;
public interface ITreeRepository
{
    Task<BasicList<TreeAutoResumeModel>> LoadAsync();
    Task SaveAsync(BasicList<TreeAutoResumeModel> list);
}