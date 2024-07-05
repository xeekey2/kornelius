using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kornelius.MVVM.Model;
using kornelius.Services;

namespace kornelius.ViewModel
{
    public partial class MainWindowVM : VM
    {
        [ObservableProperty] private bool showSettings;
        [ObservableProperty] private bool isSettingsVisible;
        [ObservableProperty] private string timerDisplayText;

        private INavigationService _navigation;
        private readonly MainVM _mainVM;

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public MainWindowVM(INavigationService navService, MainVM mainVM)
        {
            Navigation = navService;
            _mainVM = mainVM;
            navService.NavigateTo<MainVM>();

            // Subscribe to the TimerDisplayText property of MainVM
            _mainVM.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(MainVM.TimerDisplayText))
                {
                    TimerDisplayText = _mainVM.TimerDisplayText;
                }
            };
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
