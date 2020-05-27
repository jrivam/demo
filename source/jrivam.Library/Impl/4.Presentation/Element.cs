using jrivam.Library.Interface.Presentation;

namespace jrivam.Library.Impl.Presentation
{
    public class Element : IElement
    {
        public string Name { get; }

        public Element(string name)
        {
            Name = name;
        }
    }
}
