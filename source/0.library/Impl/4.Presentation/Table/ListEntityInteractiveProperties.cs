using library.Impl.Presentation;
using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Query;
using library.Interface.Presentation.Table;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace library.Impl.Domain.Table
{
    public class ListEntityInteractiveProperties<S, R, Q, T, U, V, W> : ObservableCollection<W>, IListEntityInteractiveProperties<S, R, Q, T, U, V, W>
        where S : IQueryRepositoryMethods<T, U>
        where R : IQueryLogicMethods<T, U, V>
        where Q : IQueryInteractiveMethods<T, U, V, W>
        where T : IEntity
        where U : IEntityRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>, IEntityLogicMethods<T, U, V>
        where W : class, IEntityInteractiveProperties<T, U, V>, IEntityInteractiveMethods<T, U, V, W>
    {
        public virtual ListEntityLogicProperties<S, R, T, U, V> Domains
        {
            get
            {
                return new ListEntityLogicProperties<S, R, T, U, V>().Load(this.Select(x => x.Domain));
            }
            set
            {
                value?.ForEach(x => this.Add((W)Activator.CreateInstance(typeof(W),
                           BindingFlags.CreateInstance |
                           BindingFlags.Public |
                           BindingFlags.Instance |
                           BindingFlags.OptionalParamBinding, null, new object[] { x }, CultureInfo.CurrentCulture)));
            }
        }

        public virtual ICommand AddCommand { get; set; }

        public ListEntityInteractiveProperties()
        {
        }

        public virtual ListEntityInteractiveProperties<S, R, Q, T, U, V, W> Load(Q query, int maxdepth = 1, int top = 0)
        {
            return Load(query.List(maxdepth, top).presentations);
        }
        public virtual ListEntityInteractiveProperties<S, R, Q, T, U, V, W> Load(IEnumerable<W> list)
        {
            foreach(var item in list)
                this.CommandAdd(item);

            return this;
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
