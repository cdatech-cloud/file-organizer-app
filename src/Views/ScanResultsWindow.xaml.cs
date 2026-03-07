using System.Windows;
using FileOrganizerApp.Services;

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

            this.DataContext = new
            {
                ScannedPath = _currentPath,
                ItemCount = $"Total items: {items.Count}"
            };
        }

        private void RescanButton_Click(object sender, RoutedEventArgs e)
        {
            LoadResults();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
