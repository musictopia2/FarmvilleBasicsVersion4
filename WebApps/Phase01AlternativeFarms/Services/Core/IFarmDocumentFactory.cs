namespace Phase01AlternativeFarms.Services.Core;
public interface IFarmDocumentFactory<TDocument>
    where TDocument : IFarmDocumentModel
{
    static abstract TDocument CreateEmpty(FarmKey farm);
}