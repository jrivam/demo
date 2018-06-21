using System;
using library.Interface.Data;
using library.Interface.Data.Query;
using library.Interface.Domain.Query;

namespace library.Impl.Domain.Query
{
    public abstract class AbstractQueryLogicProperties<S> : IQueryLogicProperties<S>
        where S : IQueryRepositoryProperties, new()
    {
        public virtual S Data { get; protected set; }

        public virtual IQueryColumn this[string reference]
        {
            get
            {
                return Data[reference];
            }
        }

        public AbstractQueryLogicProperties()
        {
        }
    }
}
