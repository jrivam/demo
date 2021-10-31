using jrivam.Library.Impl;
using System.Collections.Generic;

namespace jrivam.Library.Interface.Business.Table
{
    public interface ITableDomainValidation
    {
        IList<(string name, IColumnValidator validator)> Validations { get; set; }

        Result Validate();
        Result Validate(string name);
    }
}
