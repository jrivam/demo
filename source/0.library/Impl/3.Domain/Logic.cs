using library.Interface.Data.Table;
using library.Interface.Domain;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Impl.Domain
{
    public class Logic<T, U> : ILogic<T, U> 
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>, ITableRepositoryMethods<T, U>
    {
        public Logic()
        {
        }
    }
}
