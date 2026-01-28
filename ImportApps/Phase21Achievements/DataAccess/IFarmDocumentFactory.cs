namespace Phase21Achievements.DataAccess;
public interface IFarmDocumentFactory<TDocument>
    where TDocument : IFarmDocumentModel
{
    static abstract TDocument CreateEmpty(FarmKey farm);
}