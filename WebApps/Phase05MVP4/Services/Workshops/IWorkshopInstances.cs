namespace Phase05MVP4.Services.Workshops;
public interface IWorkshopRespository
{
    Task<BasicList<WorkshopAutoResumeModel>> LoadAsync();
    Task SaveAsync(BasicList<WorkshopAutoResumeModel> workshops);
}