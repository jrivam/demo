namespace Library.Interface.Persistence
{
    public interface IParameter
    {
        string Name { get; set; }
        object Value { get; set; }
    }
}
