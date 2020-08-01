using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation;
using jrivam.Library.Interface.Presentation.Table;
using System.Linq;
using System.Windows.Input;

namespace jrivam.Library.Impl.Presentation
{
    public class ListModelEdit<T, U, V, W> : ListModel<T, U, V, W>, IListModelEdit<T, U, V, W>, IStatus
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
        where W : class, ITableModel<T, U, V, W>
    {
        public virtual ICommand AddCommand { get; protected set; }

        public ListModelEdit(IListDomainEdit<T, U, V> domains = null, string name = null)
            : base(domains, 
                  name)
        {
            AddCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<W>(null, $"{Name}Add");
            }, delegate (object parameter) { return this != null; });
        }

        public virtual void ItemModify(W oldmodel, W newmodel)
        {
            ((IListDomainEdit<T, U, V>)Domains).ItemModify(oldmodel.Domain, newmodel.Domain);
        }
        public virtual bool ItemAdd(W model)
        {
            if (model != null)
            {
                if (((IListDomainEdit<T, U, V>)Domains).ItemAdd(model.Domain))
                {
                    this.Add(model);

                    TotalRecords();
                    return true;
                }
            }

            return false;
        }
        public virtual bool ItemRemove(W model)
        {
            if (model != null)
            {
                if (((IListDomainEdit<T, U, V>)Domains).ItemRemove(model.Domain))
                {
                    this.Remove(model);

                    TotalRecords();
                    return true;
                }
            }

            return false;
        }

        public virtual (Result result, W model) CommandSave((CommandAction action, (Result result, W model) operation) message)
        {
            return message.operation;
        }
        public virtual (Result result, W model) CommandErase((CommandAction action, (Result result, W model) operation) message)
        {
            if (message.operation.result.Success)
                ItemRemove(this.FirstOrDefault(x => x.Domain?.Data?.Entity?.Id == message.operation.model?.Domain?.Data?.Entity?.Id));

            return message.operation;
        }

    }
}
