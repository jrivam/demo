using System.Collections.Generic;

namespace library.Interface.Presentation.Table
{
    public interface ITableModelValidation
    {
        Dictionary<string, string> Validations { get; set; }

        bool Validate(string key, string value);
    }
}
