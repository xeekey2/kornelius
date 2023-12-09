using kornelius.ViewModels;
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
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize your main window here
            var mainWindow = new MainWindow();
            var viewModel = new JiraViewModel();
            mainWindow.DataContext = viewModel;

            await viewModel.InitializeAsync();

            NotifyIcon trayIcon = new NotifyIcon();
            trayIcon.Icon = Icons.MyIcon_ico;
            trayIcon.Visible = true;
            trayIcon.Click += delegate (object sender, EventArgs args)
            {
                // Ensure the main window is not already visible
                if (mainWindow.Visibility != Visibility.Visible)
                {
                    // Set window properties to make it look like a popup
                    mainWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                    mainWindow.Left = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - mainWindow.Width;
                    mainWindow.Top = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - mainWindow.Height;

                    // Show the main window without activating it
                    mainWindow.Show();
                    mainWindow.Visibility = Visibility.Visible;

                    // Optional: Set topmost to true to ensure the popup always appears on top
                    mainWindow.Topmost = true;
                    // Then set it back to false so that the window does not stay on top
                    mainWindow.Topmost = false;
                }
                else
                {
                    // If the window is already visible, hide it
                    mainWindow.Visibility = Visibility.Hidden;
                }
            };

            // Create the tray icon
            this.MainWindow = mainWindow;
        }
    }

}
