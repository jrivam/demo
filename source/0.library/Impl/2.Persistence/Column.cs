﻿using Library.Interface.Data;
using System;

namespace Library.Impl.Data
{
    public class Column<A> : IColumn
    {
        public virtual Type Type
        {
            get
            {
                return typeof(A);
            }
        }

        public virtual Description Description { get; }

        public Column(string name, string reference)
        {
            Description = new Description(name, reference);
        }
    }
}