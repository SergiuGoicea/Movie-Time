using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MovieTime.Data.Interfaces;

namespace MovieTime.Data.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected IConnectivityService connectivityService;
        public bool IsConnectedToInternet => connectivityService.IsConnected;
        private bool isBusy;
        public bool IsBusy { get => isBusy; set => SetProperty(ref isBusy, value); }
        public BaseViewModel(IConnectivityService connectivityService)
        {
            this.connectivityService = connectivityService;
        }

        public virtual void OnAppearing()
        {
            connectivityService.ConnectivityChanged += ConnectivityService_ConnectivityChanged;
        }


        public virtual void OnDisappearing()
        {
            connectivityService.ConnectivityChanged -= ConnectivityService_ConnectivityChanged;
        }

        private void ConnectivityService_ConnectivityChanged(object sender, bool isConnected)
        {
            InternalOnConnectivityChanged(isConnected);
        }

        protected virtual void InternalOnConnectivityChanged(bool isConnected)
        {
            OnPropertyChanged(nameof(IsConnectedToInternet));
        }


        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;

            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        
    }
}
