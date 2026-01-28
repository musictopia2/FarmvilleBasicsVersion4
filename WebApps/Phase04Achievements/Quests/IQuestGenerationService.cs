namespace Phase04Achievements.Quests;
public interface IQuestGenerationService
{
    QuestInstanceModel CreateQuest(int currentLevel,
        BasicList<ItemPlanModel> eligibleItems,
        BasicList<QuestInstanceModel> existingBoard);
}