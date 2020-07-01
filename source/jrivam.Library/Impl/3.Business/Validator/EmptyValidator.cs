﻿using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;

namespace jrivam.Library.Impl.Business.Validator
{
    public class EmptyValidator : ColumnValidator
    {
        public EmptyValidator(IColumnTable column)
            : base(column)
        {
        }

        public override Result Validate()
        {
            var validate = base.Validate();

            if (validate.Success)
            {
                if (string.IsNullOrWhiteSpace(_column.Value?.ToString()))
                {
                    return new Result(
                        new ResultMessage()
                            {
                                Category = ResultCategory.Error,
                                Name = "EmptyValidation",
                                Description =  $"{_column.Description.Name} must have a value"
                            }
                        );
                }
            }

            return validate;
        }
    }
}
