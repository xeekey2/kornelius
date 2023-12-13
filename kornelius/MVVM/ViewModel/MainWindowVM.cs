using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kornelius.MVVM.Model;
using kornelius.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kornelius.ViewModel
{
    public partial class MainWindowVM : VM
    {
        [ObservableProperty] private bool showSettings;
        [ObservableProperty] private bool isSettingsVisible;

        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }
        public MainWindowVM(INavigationService navService)
        {
            Navigation = navService;
            navService.NavigateTo<MainVM>();
        }

        [RelayCommand]
        private void ToggleSettings()
        {
            IsSettingsVisible = !IsSettingsVisible;
            if (IsSettingsVisible)
            {
                Navigation.NavigateTo<MainVM>();

            }
            else
            {
                Navigation.NavigateTo<SettingsVM>();
            }
        }
    }
}
