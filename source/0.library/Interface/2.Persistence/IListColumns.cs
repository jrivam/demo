namespace Library.Interface.Data
{
    public interface IListColumns<T>
    {
        T this[string reference] { get; }
    }
}
