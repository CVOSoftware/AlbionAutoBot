using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AlbionAutoBot.App.ViewModel.Base
{
    internal class BindableBaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string prop = " ")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        protected bool SetValue<T>(ref T local, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (Equals(local, newValue))
            {
                return false;
            }

            local = newValue;
            OnPropertyChanged(propertyName);

            return true;
        }

        protected void UpdateUI(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}
