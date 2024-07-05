using kornelius.Model;
using kornelius.MVVM.Model;
using kornelius.Services;
using kornelius.View;
using kornelius.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace kornelius
{
    public partial class App : System.Windows.Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainWindowVM>()
            });

            services.AddSingleton<MainWindowVM>();
            services.AddSingleton<MainVM>();
            services.AddSingleton<SettingsVM>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<Func<Type, VM>>(serviceProvider => viewModelType => (VM)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }


        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            NotifyIcon trayIcon = new NotifyIcon();
            trayIcon.Icon = Icons.MyIcon_ico;
            trayIcon.Visible = true;
            trayIcon.Click += delegate (object sender, EventArgs args)
            {
                if (mainWindow.Visibility != Visibility.Visible)
                {
                    mainWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                    mainWindow.Left = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - mainWindow.Width;
                    mainWindow.Top = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - mainWindow.Height;

                    mainWindow.Show();
                    mainWindow.Visibility = Visibility.Visible;

                    mainWindow.Topmost = true;
                    mainWindow.Topmost = false;
                }
                else
                {
                    mainWindow.Visibility = Visibility.Hidden;
                }
            };

            this.MainWindow = mainWindow;
        }
    }

}
