using CommunityToolkit.Mvvm.ComponentModel;
using kornelius.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kornelius.Services
{
    public interface INavigationService
    {
        VM CurrentView { get; }
        void NavigateTo<T>() where T : VM;
    }

    public class NavigationService : ObservableObject, INavigationService
    {
        private VM _currentView;
        private readonly Func<Type, VM> _viewModelFactory;

        public VM CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public void NavigateTo<TVM>() where TVM : VM
        {
            VM viewModel = _viewModelFactory.Invoke(typeof(TVM));
            CurrentView = viewModel;
        }

        public NavigationService(Func<Type, VM> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }
    }
}
