using System;
using System.Windows;

namespace FileOrganizerApp
{
    public partial class App : Application
    {
        public App()
        {
            // Add global exception handler
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Dispatcher.UnhandledException += Dispatcher_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show($"An unexpected error occurred:\n\n{ex?.Message}\n\n{ex?.StackTrace}", 
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"An unexpected error occurred:\n\n{e.Exception.Message}\n\n{e.Exception.StackTrace}", 
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
