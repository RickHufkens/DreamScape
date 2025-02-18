using DreamScape.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace DreamScape
{
    public sealed partial class InventoryPage : Page
    {
        // Simulated data for demo purposes
        private ObservableCollection<UserItem> UserItems;

        public InventoryPage()
        {
            this.InitializeComponent();
            LoadUserItems();
        }

        // Load items for the logged-in user
        private void LoadUserItems()
        {
            // Simulate getting the logged-in user's ID (this would normally come from the authentication system)
            int loggedInUserId = 1; // Example user ID, replace with actual logged-in user ID

            // Simulate a list of items (replace this with actual data loading logic)
            var allUserItems = GetUserItemsFromDatabase();

            // Filter by the logged-in user's ID
            UserItems = new ObservableCollection<UserItem>(allUserItems.Where(ui => ui.UserId == loggedInUserId));

            // Bind the data to the ListView
            InventoryListView.ItemsSource = UserItems;
        }

        // This is a placeholder for the database query (replace with actual database access)
        private List<UserItem> GetUserItemsFromDatabase()
        {
            return new List<UserItem>
            {
                new UserItem { Id = 1, UserId = 1, ItemId = 101, Quantity = 5, Item = new Item { Id = 101, Name = "Sword" } },
                new UserItem { Id = 2, UserId = 1, ItemId = 102, Quantity = 3, Item = new Item { Id = 102, Name = "Shield" } },
                new UserItem { Id = 3, UserId = 1, ItemId = 103, Quantity = 2, Item = new Item { Id = 103, Name = "Potion" } },
            };
        }
    }
}