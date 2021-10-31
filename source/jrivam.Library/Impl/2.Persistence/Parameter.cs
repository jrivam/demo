using jrivam.Library.Interface.Persistence;

namespace jrivam.Library.Impl.Persistence
{
    public class Parameter : IParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
