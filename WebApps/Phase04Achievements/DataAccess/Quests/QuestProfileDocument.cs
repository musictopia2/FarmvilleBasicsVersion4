namespace Phase04Achievements.DataAccess.Quests;
public class QuestProfileDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; init; }
    public BasicList<QuestInstanceModel> Quests { get; set; } = []; //must be blank since its resetting.
    //any other player stuff needed will be in the real project, not here.

}