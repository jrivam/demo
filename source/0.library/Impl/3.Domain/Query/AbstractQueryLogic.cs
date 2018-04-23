using library.Interface.Data.Model;
using library.Interface.Data.Query;
using library.Interface.Domain.Model;
using library.Interface.Domain.Query;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Impl.Domain.Query
{
    public abstract class AbstractQueryLogic<S, T, U, V> : AbstractQueryState<S>, IQueryLogic<T, U, V>
        where S : IQueryTable, IQueryRepository<T, U>, new()
        where T : IEntity
        where U : IEntityTable<T>, IEntityRepository<T, U>
        where V : IEntityState<T, U>
    {
        public ILogicQuery<T, U, V> _logic { get; protected set; }

        public AbstractQueryLogic(ILogicQuery<T, U, V> logic)
            : base()
        {
            _logic = logic;
        }

        public virtual (Result result, V domain) Retrieve(int maxdepth = 1, V domain = default(V))
        {
            return _logic.Retrieve(Data, maxdepth, domain);
        }
        public virtual (Result result, IEnumerable<V> domains) List(int maxdepth = 1, int top = 0, IList<V> domains = null)
        {
            return _logic.List(Data, maxdepth, top, domains);
        }
    }
}
