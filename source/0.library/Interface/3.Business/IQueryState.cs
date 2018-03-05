using library.Interface.Data;

namespace library.Interface.Business
{
    public interface IQueryState<T> where T : IQueryTable
    {
        T Data { get; set; }
    }
}
