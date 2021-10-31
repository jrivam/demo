using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace jrivam.Library.Impl.Presentation
{
    public abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged = delegate { };
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
