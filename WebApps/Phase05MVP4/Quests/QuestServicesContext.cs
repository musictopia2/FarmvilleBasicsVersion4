namespace Phase05MVP4.Quests;
public class QuestServicesContext
{
    public required IQuestProfile QuestProfile { get; init; }
    public required IQuestGenerationService QuestGenerationService { get; init; }
}