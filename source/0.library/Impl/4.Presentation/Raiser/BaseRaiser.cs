using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation.Raiser;
using Library.Interface.Presentation.Table;

namespace Library.Impl.Presentation.Raiser
{
    public class BaseRaiser<T, U, V, W> : IRaiser<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        public virtual void Clear(W model)
        {
           //model.Validations = new Dictionary<string, string>();

            model.OnPropertyChanged("Validations");
        }

        public virtual void Raise(W model, int maxdepth = 1, int depth = 0)
        {
            foreach (var element in model.Elements)
            {
                model.OnPropertyChanged(element.Name);
            }
        }
        public virtual void RaiseX(W model, int maxdepth = 1, int depth = 0)
        {
        }
    }
}
