namespace jrivam.Library.Impl
{
    public enum ResultCategory
    {
        Information = 0,
        Warning = 1,
        Error = 2,
        Exception = 4,
        OnlyErrors = Error | Exception,
        All = Information | Warning | Error | Exception
    }
}
