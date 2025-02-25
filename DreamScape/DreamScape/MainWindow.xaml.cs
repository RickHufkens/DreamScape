using DreamScape.Data;
using Microsoft.UI.Windowing;
using Microsoft.UI;
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
using WinRT.Interop;

namespace DreamScape
{
    public sealed partial class MainWindow : Window
    {
        public static int LoggedInUserId { get; set; }
        public static Frame mainFrame { get; private set; }

        public MainWindow()
        {
            this.InitializeComponent();  // This should be here
            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }

            mainFrame = MainFrame;
            MainFrame.Navigate(typeof(LoginPage));

            // Maximize window by setting the size
            var hWnd = WindowNative.GetWindowHandle(this);
            var windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = AppWindow.GetFromWindowId(windowId);
            appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);  // Fullscreen mode

            // Optional: remove the title bar
            appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
        }
    }
}
