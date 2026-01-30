namespace Phase05MVP4.Services.Worksites;
public class WorksiteServicesContext
{
    required public IWorksiteRegistry WorksiteRegistry { get; init; }
    required public IWorksiteRepository WorksiteRepository { get; init; }
    required public IWorksiteCollectionPolicy WorksiteCollectionPolicy { get; init; }
}