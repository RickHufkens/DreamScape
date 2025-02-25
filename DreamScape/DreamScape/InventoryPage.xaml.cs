using DreamScape.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DreamScape
{
    public sealed partial class InventoryPage : Page
    {
        private ObservableCollection<UserItem> UserItems;

        public InventoryPage()
        {
            this.InitializeComponent();
            LoadUserItems();
        }

        private void LoadUserItems()
        {
            int loggedInUserId = MainWindow.LoggedInUserId; // Retrieve the logged-in user ID

            // Fetch user items from the database based on the logged-in user ID
            using (var db = new AppDbContext())
            {
                var userItems = db.UserItems
                    .Where(ui => ui.UserId == loggedInUserId)  // Filter based on logged-in user ID
                    .Include(ui => ui.Item) // Include the item details (e.g., Name, Description, etc.)
                    .ToList();

                // If no items found in the database, use test data (fallback)
                if (!userItems.Any())
                {
                    // Add test items directly from the database if real data is unavailable
                    var testItems = new List<UserItem>
                    {
                        new UserItem { Id = 1, UserId = loggedInUserId, ItemId = 1, Quantity = 3 },
                        new UserItem { Id = 2, UserId = loggedInUserId, ItemId = 2, Quantity = 5 },
                        new UserItem { Id = 3, UserId = loggedInUserId, ItemId = 3, Quantity = 2 },
                        new UserItem { Id = 4, UserId = loggedInUserId, ItemId = 4, Quantity = 7 }
                    };

                    userItems = testItems;
                }

                // Map UserItems to an ObservableCollection for data binding
                UserItems = new ObservableCollection<UserItem>(userItems);

                // Dynamically update any dependent UI component (like ListView)
                InventoryListView.ItemsSource = UserItems;
            }
        }

        // Navigation methods
        private void NavigateToInventory(object sender, RoutedEventArgs e)
        {
            // Prevent navigating to the current page
            if (this.Frame.CurrentSourcePageType == typeof(InventoryPage))
            {
                return; // Do nothing if already on this page
            }

            this.Frame.Navigate(typeof(InventoryPage));
        }

        private void NavigateToTrades(object sender, RoutedEventArgs e)
        {
            // Prevent navigating to the current page
            if (this.Frame.CurrentSourcePageType == typeof(TradesPage))
            {
                return; // Do nothing if already on this page
            }

            this.Frame.Navigate(typeof(TradesPage));
        }

        private void NavigateToProfile(object sender, RoutedEventArgs e)
        {
            // Prevent navigating to the current page
            if (this.Frame.CurrentSourcePageType == typeof(ProfilePage))
            {
                return; // Do nothing if already on this page
            }

            this.Frame.Navigate(typeof(ProfilePage));
        }

        private void NavigateToLogin(object sender, RoutedEventArgs e)
        {
            // Prevent navigating to the current page
            if (this.Frame.CurrentSourcePageType == typeof(LoginPage))
            {
                return; // Do nothing if already on this page
            }

            // Log out logic here if needed
            this.Frame.Navigate(typeof(LoginPage));
        }
    }
}
