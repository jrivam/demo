using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Impl.Business.Validator
{
    public class ColumnValidator : IColumnValidator
    {
        protected readonly IColumnTable _column;

        public ColumnValidator(IColumnTable column)
        {
            _column = column;
        }

        public virtual Result Validate()
        {
            return (new Result() { Success = true });
        }
    }
}
