namespace Phase18AlternativeFarms.DataAccess;
public interface IFarmDocumentFactory<TDocument>
    where TDocument : IFarmDocument
{
    static abstract TDocument CreateEmpty(FarmKey farm);
}