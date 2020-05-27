using jrivam.Library.Impl.Presentation;

namespace jrivam.Library.Interface.Presentation.Table
{
    public interface ITableModelElements
    {
        IElement this[string name] { get; }
        ListElements<IElement> Elements { get; set; }
    }
}
