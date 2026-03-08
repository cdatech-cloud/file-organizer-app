using System.Windows;
using System.Windows.Forms;
using FileOrganizerApp;
using FileOrganizerApp.Models;
using FileOrganizerApp.Services;

namespace FileOrganizerApp.Views
{
    public partial class MainWindow : Window
    {
        private FileScanner _fileScanner;
        private string _currentScanPath;

        public MainWindow()
        {
            InitializeComponent();
            _fileScanner = new FileScanner();
        }

        private void ScanSettings_Click(object sender, RoutedEventArgs e)
        {
            StartScan();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CategoryManager_Click(object sender, RoutedEventArgs e)
        {
            var categoryWindow = new CategoryManagementWindow();
            categoryWindow.ShowDialog();
        }

        private void ViewTasks_Click(object sender, RoutedEventArgs e)
        {
            var tasksWindow = new TasksWindow(AppContext.TaskManager);
            tasksWindow.ShowDialog();
        }

        private void StartScan_Click(object sender, RoutedEventArgs e)
        {
            StartScan();
        }

        private void StartScan()
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a folder or drive to scan";
                folderDialog.ShowNewFolderButton = false;

                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _currentScanPath = folderDialog.SelectedPath;
                    LoadScanResults();
                    
                    // Switch to Scan Results tab
                    ScanResultsTab.IsSelected = true;
                }
            }
        }

        private void ScheduleScan_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Schedule Scan - Not yet implemented", "File Organizer");
        }

        private void LoadScanResults()
        {
            var items = _fileScanner.ScanDirectChildren(_currentScanPath);
            ItemsListView.ItemsSource = items;
            
            ScannedPathTextBlock.Text = _currentScanPath;
            ItemCountTextBlock.Text = $"Total items: {items.Count}";
        }

        private void RescanButton_Click(object sender, RoutedEventArgs e)
        {
            LoadScanResults();
        }

        private void ItemsListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ItemsListView.SelectedItem is FileSystemItem selectedItem)
            {
                try
                {
                    var actionDialog = new ItemActionDialog(selectedItem);
                    if (actionDialog.ShowDialog() == true)
                    {
                        // Refresh the list after action is completed
                        LoadScanResults();
                    }
                    
                    // Deselect the item after action is complete
                    ItemsListView.SelectedItem = null;
                }
                catch (System.Exception ex)
                {
                    System.Windows.MessageBox.Show($"Error opening item action dialog: {ex.Message}\n\n{ex.StackTrace}", 
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    // Deselect on error
                    ItemsListView.SelectedItem = null;
                }
            }
        }
    }
}