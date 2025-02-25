using DreamScape.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DreamScape
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ShowError("Please enter both username and password.");
                return;
            }

            using (var db = new AppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Name == username);

                if (user != null && VerifyPassword(password, user.Password))
                {
                    // Set the LoggedInUserId after successful login
                    MainWindow.LoggedInUserId = user.Id;

                    // Navigate to InventoryPage
                    this.Frame.Navigate(typeof(InventoryPage));
                }
                else
                {
                    ShowError("Invalid username or password.");
                }
            }
        }

        private void ShowError(string message)
        {
            ErrorMessage.Text = message;
            ErrorMessage.Visibility = Visibility.Visible;
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] enteredBytes = Encoding.UTF8.GetBytes(enteredPassword);
                byte[] enteredHash = sha256.ComputeHash(enteredBytes);

                return Convert.ToBase64String(enteredHash) == storedHash;
            }
        }
    }
}
