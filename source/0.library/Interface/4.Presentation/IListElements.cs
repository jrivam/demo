namespace Library.Interface.Presentation
{
    public interface IListElements<T>
    {
        T this[string name] { get; }
    }
}
