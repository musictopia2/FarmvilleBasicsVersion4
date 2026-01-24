namespace Phase01AlternativeFarms.DataAccess.InstantUnlimited;
public class InstantUnlimitedFactory : IInstantUnlimitedFactory
{
    InstantUnlimitedServicesContext IInstantUnlimitedFactory.GetInstantUnlimitedServices(FarmKey farm)
    {
        return new()
        {
            InstantUnlimitedProfile = new InstantUnlimitedInstanceDatabase(farm)
        };
    }
}