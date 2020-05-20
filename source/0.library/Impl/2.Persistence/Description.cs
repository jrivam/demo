namespace Library.Impl.Persistence
{
    public class Description
    {
        public string Name { get; set; }
        public string DbName { get; set; }

        public Description(string name, string dbname)
        {
            Name = name;
            DbName = dbname;
        }
    }
}
