using System;
using MovieTime.Data.Interfaces;

namespace MovieTime.Data.Services
{
    internal class ConnectivityService:IConnectivityService
    {
        public ConnectivityService()
        {
            Xamarin.Essentials.Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, Xamarin.Essentials.ConnectivityChangedEventArgs e)
        {
            ConnectivityChanged?.Invoke(this, IsConnected);
        }

        public bool IsConnected => Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet;

        public event EventHandler<bool> ConnectivityChanged;
    }
}
