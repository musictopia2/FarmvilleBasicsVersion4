namespace Phase04Achievements.Services.Core;
public interface IFarmDocumentFactory<TDocument>
    where TDocument : IFarmDocumentModel
{
    static abstract TDocument CreateEmpty(FarmKey farm);
}