using System;
using MovieTime.Data.Interfaces;
using MovieTime.Data.Services;
using Unity;
using Unity.ServiceLocation;

namespace MovieTime.Data.ViewModel
{
    public sealed class ViewModelLocator
    {
        private static readonly Lazy<ViewModelLocator> lazy = new Lazy<ViewModelLocator>(() => new ViewModelLocator());
        public static ViewModelLocator Instance { get { return lazy.Value; } }

        UnityContainer container;
        UnityServiceLocator resolver;

        private ViewModelLocator()
        {
            container = new UnityContainer();
            resolver = new UnityServiceLocator(container);

            container.RegisterSingleton<IMovieService, MovieService>();
            container.RegisterSingleton<IConnectivityService, ConnectivityService>();
        }

        public T GetInstance<T>()
        {
            //resolver.GetInstance<>
            return resolver.GetInstance<T>();
        }
    }
}
