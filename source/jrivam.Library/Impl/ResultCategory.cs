namespace jrivam.Library.Impl
{
    public enum ResultCategory
    {
        Information = 1,
        Warning = 2,
        Error = 4,
        Exception = 8,
        OnlyErrors = Error | Exception,
        OnlyInformation = Information | Warning,
        All = OnlyErrors | OnlyInformation
    }
}
