using library.Impl.Presentation;
using library.Interface.Data.Model;
using library.Interface.Data.Query;
using library.Interface.Domain.Model;
using library.Interface.Domain.Query;
using library.Interface.Entities;
using library.Interface.Presentation.Model;
using library.Interface.Presentation.Query;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace library.Impl.Domain.Model
{
    public class ListEntityView<S, R, Q, T, U, V, W> : ObservableCollection<W>, IListEntityView<S, R, Q, T, U, V, W>
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>, IEntityLogic<T, U, V>
        where W : IEntityView<T, U, V>, IEntityInteractive<T, U, V, W>
        where S : IQueryRepository<T, U>
        where R : IQueryLogic<T, U, V>
        where Q : IQueryInteractive<T, U, V, W>
    {
        public virtual ListEntityState<S, R, T, U, V> Domains
        {
            get
            {
                var list = new ListEntityState<S, R, T, U, V>();
                this.ToList().ForEach(x => list.Add(x.Domain));
                return list;
                //return new ListEntityState<S, R, T, U, V>().Load(this.Select(x => x.Domain).Cast<V>());
            }
        }
        public virtual V Domain
        {
            get
            {
                return this.Count > 0 ? this[0].Domain : default(V);
                //return new ListEntityState<S, R, T, U, V>().Load(this.Select(x => x.Domain).Cast<V>());
            }
        }

        public virtual ICommand AddCommand { get; set; }

        public ListEntityView()
        {
        }

        public virtual ListEntityView<S, R, Q, T, U, V, W> Load(Q query, int maxdepth = 1, int top = 0)
        {
            return Load(query.List(maxdepth, top).presentations);
        }
        public virtual ListEntityView<S, R, Q, T, U, V, W> Load(IEnumerable<W> list)
        {
            foreach(var item in list)
                this.CommandAdd(item);

            return this;
        }

        public virtual Result Save()
        {
            var save = new Result() { Success = true };

            foreach (var domain in this)
            {
                save.Append(domain.Save().result);

                if (!save.Success) break;
            }

            return save;
        }
        public virtual Result Erase()
        {
            var erase = new Result() { Success = true };

            foreach (var domain in this)
            {
                erase.Append(domain.Erase().result);

                if (!erase.Success) break;
            }

            return erase;
        }

        public virtual void CommandLoad((CommandAction action, (Result result, W presentation) operation) message)
        {
        }
        public virtual void CommandSave((CommandAction action, (Result result, W presentation) operation) message)
        {
        }
        public virtual void CommandErase((CommandAction action, (Result result, W presentation) operation) message)
        {
            if (message.operation.result.Success)
                if (message.operation.presentation?.Domain.Data.Entity.Id != null)
                    this.Remove(message.operation.presentation);
        }

        public virtual void CommandAdd(W presentation)
        {
            if (presentation.Domain.Data.Entity?.Id != null)
                this.Add(presentation);
        }
        public virtual void CommandEdit((W oldvalue, W newvalue) message)
        {
            if (this.Count > 0)
            {
                var i = this.IndexOf(message.oldvalue);
                if (i >= 0)
                    this[i] = message.newvalue;
            }
        }
    }
}
