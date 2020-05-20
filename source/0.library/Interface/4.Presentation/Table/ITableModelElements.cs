using Library.Impl.Presentation;

namespace Library.Interface.Presentation.Table
{
    public interface ITableModelElements
    {
        IElement this[string name] { get; }
        ListElements<IElement> Elements { get; set; }
    }
}
