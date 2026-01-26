namespace Phase02AdvancedUpgrades.Quests;
public interface IQuestFactory
{
    QuestServicesContext GetQuestServices(FarmKey farm, CropManager cropManager, TreeManager treeManager);
}