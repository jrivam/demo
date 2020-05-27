namespace jrivam.Library.Interface.Presentation
{
    public interface IListElements<T>
    {
        T this[string name] { get; }
    }
}
