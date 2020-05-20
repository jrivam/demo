using Library.Interface.Presentation;

namespace Library.Impl.Presentation
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
