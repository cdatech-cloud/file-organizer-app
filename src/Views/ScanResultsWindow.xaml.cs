using System.Windows;
using System.Windows.Input;
using FileOrganizerApp.Models;
using FileOrganizerApp.Services;
using FileOrganizerApp.ViewModels;

namespace FileOrganizerApp.Views
{
    public partial class ScanResultsWindow : Window
    {
        private FileScanner _fileScanner;
        private string _currentPath;

        public ScanResultsWindow(string path)
        {
            InitializeComponent();
            _fileScanner = new FileScanner();
            _currentPath = path;
            LoadResults();
        }

        private void LoadResults()
        {
            var items = _fileScanner.ScanDirectChildren(_currentPath);
            ItemsListView.ItemsSource = items;

            this.DataContext = new ScanResultsWindowViewModel(_currentPath, $"Total items: {items.Count}");
        }

        private void RescanButton_Click(object sender, RoutedEventArgs e)
        {
            LoadResults();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ItemsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ItemsListView.SelectedItem is FileSystemItem selectedItem)
            {
                try
                {
                    var actionDialog = new ItemActionDialog(selectedItem);
                    if (actionDialog.ShowDialog() == true)
                    {
                        // Refresh the list after action is completed
                        LoadResults();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Error opening item action dialog: {ex.Message}\n\n{ex.StackTrace}", 
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

