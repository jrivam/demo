﻿namespace Library.Interface.Persistence
{
    public interface IListColumns<T>
    {
        T this[string reference] { get; }
    }
}
