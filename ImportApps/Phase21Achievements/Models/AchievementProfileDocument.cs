namespace Phase21Achievements.Models;
public class AchievementProfileDocument : IFarmDocumentModel, IFarmDocumentFactory<AchievementProfileDocument>
{
    public required FarmKey Farm { get; set; }
    public AchievementProfileModel Profile { get; set; } = new();
    public static AchievementProfileDocument CreateEmpty(FarmKey farm)
    {
        return new()
        {
            Farm = farm,

        };
    }
}