namespace jrivam.Library.Impl
{
    public enum ResultCategory
    {
        Information = 1,
        Warning = 2,
        Error = 4,
        Exception = 8,
        Successful = Information | Warning,
        NotSuccessful = Error | Exception,
        All = Successful | NotSuccessful
    }
}
