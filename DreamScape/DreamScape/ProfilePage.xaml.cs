using DreamScape.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;

namespace DreamScape
{
    public sealed partial class ProfilePage : Page
    {
        public User User { get; set; }

        public ProfilePage()
        {
            this.InitializeComponent();
            LoadUserProfile();
        }

        private void LoadUserProfile()
        {
            int loggedInUserId = MainWindow.LoggedInUserId; // Retrieve the logged-in user ID

            // Fetch the user data from the database based on the logged-in user ID
            using (var db = new AppDbContext())
            {
                User = db.Users
                    .Where(u => u.Id == loggedInUserId) // Filter by logged-in user ID
                    .Include(u => u.UserItems) // Include the user items
                    .Include(u => u.SentTrades) // Include the sent trades
                    .ThenInclude(t => t.Item) // Include item details for trades (optional)
                    .FirstOrDefault();

                if (User == null)
                {
                    // Handle the case where the user is not found
                    // For example, show an error message or navigate to the login page
                }
            }

            // Set the data context for binding
            this.DataContext = this;
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
