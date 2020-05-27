using System;
using System.Collections.Generic;

namespace jrivam.Library.Impl.Presentation
{
    public class Validatoring : NotifyPropertyChanged
    {
        public List<(string name, Action method)> Validating = new List<(string, Action)>();

        public Validatoring()
        {
            foreach (var action in Validating)
            {
                action.method.Invoke();
            }
        }
    }
}
