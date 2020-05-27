using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation.Raiser;
using jrivam.Library.Interface.Presentation.Table;
using System.Collections.Generic;

namespace jrivam.Library.Impl.Presentation
{
    public class InteractiveRaiser<T, U, V, W> : Interactive<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        protected readonly IRaiser<T, U, V, W> _raiser;

        public InteractiveRaiser(IRaiser<T, U, V, W> raiser)
            : base()
        {
            _raiser = raiser;
        }

        protected virtual W Raise(W model, int maxdepth = 1)
        {
            _raiser.Raise(model, maxdepth, 0);

            return model;
        }

        protected virtual IEnumerable<W> RaiseDomains(IEnumerable<V> domains, int maxdepth = 1)
        {
            foreach (var domain in domains)
            {
                var presentation = Presentation.HelperTableInteractive<T, U, V, W>.CreateModel(domain, maxdepth);

                //Raise(presentation, maxdepth);

                yield return presentation;
            }
        }
    }
}
