using library.Interface.Data;

namespace library.Interface.Domain
{
    public interface IQueryState<T> where T : IQueryTable
    {
        T Data { get; }
    }
}
