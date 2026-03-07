using System;
using System.Windows;
using FileOrganizerApp.Views;

namespace FileOrganizerApp
{
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            App app = new App();
            app.InitializeComponent();
            app.Run(new MainWindow());
        }
    }
}