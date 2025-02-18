using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.NetworkOperators;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DreamScape
{
    public sealed partial class DashboardPage : Window
    {
        public DashboardPage()
        {
            this.InitializeComponent();
            // After login, navigate to the Inventory page by default
            MainFrame.Navigate(typeof(InventoryPage));
        }

        // Handles the navigation button clicks
        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the page to navigate to based on the button's Tag
            string tab = (sender as Button).Tag.ToString();

            switch (tab)
            {
                case "Profile":
                    MainFrame.Navigate(typeof(ProfilePage)); // Replace with your actual profile page
                    break;
                case "Inventory":
                    MainFrame.Navigate(typeof(InventoryPage)); // Replace with your actual inventory page
                    break;
                case "Trades":
                    MainFrame.Navigate(typeof(TradesPage)); // Replace with your actual trades page
                    break;
                case "Logout":
                    // Handle logout logic (if needed)
                    MainFrame.Navigate(typeof(LoginPage)); // Navigate back to the login page
                    break;
            }
        }
    }
}