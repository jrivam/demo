using Library.Interface.Persistence;

namespace Library.Impl.Persistence
{
    public class Parameter : IParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
