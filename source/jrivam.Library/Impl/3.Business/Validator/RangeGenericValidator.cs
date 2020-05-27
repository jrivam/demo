using jrivam.Library.Interface.Persistence.Table;
using System;
using System.Collections.Generic;

namespace jrivam.Library.Impl.Business.Validator
{
    public class RangeGenericValidator<T> : ColumnValidator
    {
        protected readonly T _min;
        protected readonly T _max;

        public RangeGenericValidator(IColumnTable column,
            T min = default(T), T max = default(T))
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
                if ((!EqualityComparer<T>.Default.Equals(_min, default(T)) && Comparer<T>.Default.Compare((T)Convert.ChangeType(_column.Value, typeof(T)), _min) < 0)
                    || (!EqualityComparer<T>.Default.Equals(_max, default(T)) && Comparer<T>.Default.Compare((T)Convert.ChangeType(_column.Value, typeof(T)), _max) > 0))
                {
                    return new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "RangeValidation", $"{_column.Description.Name} must be between {_min.ToString()} and {_max.ToString()}") } };
                }
            }

            return validate;
        }
    }
}
