using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GestiCoop.UI.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string n = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));

        protected bool Set<T>(ref T f, T v, [CallerMemberName] string n = null)
        {
            if (Equals(f, v)) return false;
            f = v;
            OnPropertyChanged(n);
            return true;
        }
    }
}