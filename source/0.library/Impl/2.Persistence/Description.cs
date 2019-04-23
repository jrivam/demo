namespace Library.Impl.Persistence
{
    public class Description
    {
        public string Name { get; set; }
        public string Reference { get; set; }

        public Description(string name, string reference)
        {
            Name = name;
            Reference = reference;
        }
    }
}
