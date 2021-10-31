using jrivam.Library.Interface.Presentation.Table;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace jrivam.Library.Impl.Presentation
{
    public class TableModelValidation : NotifyDataErrorInfo, ITableModelValidation
    {
        //public Dictionary<string, string> Validations { get; set; } = new Dictionary<string, string>();
        //public bool Validate(string key, string value)
        //{
        //    var contains = Validations.ContainsKey(key);

        //    if ((contains && Validations[key] != value) || (!contains && value != string.Empty))
        //    {
        //        Validations[key] = value;

        //        OnPropertyChanged("Validations");

        //        return true;
        //    }

        //    return false;
        //}
        //public List<(string name, Action method)> Validating = new List<(string, Action)>();
    }
}
