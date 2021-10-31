using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace jrivam.Library.Impl.Business.Validator
{
    public class RangeValidator : ColumnValidator
    {
        protected readonly int? _min;
        protected readonly int? _max;

        public RangeValidator(IColumnTable column,
            int? min = null, int? max = null)
            : base(column)
        {
            _min = min;
            _max = max;
        }

        public override Result Validate()
        {
            var validate = base.Validate();

            if (validate.Success)
            {
                if ((_min != null && (int)_column.Value < _min)
                    || (_max != null && (int)_column.Value > _max))
                {
                    return new Result(
                        new ResultMessage()
                            {
                                Category = ResultCategory.Error,
                                Name = "RangeValidation",
                                Description =  $"{_column.Description.Name} must be between {_min} and {_max}"
                            }
                        );
                }
            }

            return validate;
        }
    }
}
