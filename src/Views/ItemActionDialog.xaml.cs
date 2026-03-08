using System.Windows;
using FileOrganizerApp.Models;
using FileOrganizerApp.Services;

namespace FileOrganizerApp.Views
{
    public partial class ItemActionDialog : Window
    {
        private FileSystemItem _item;
        private FileOrganizer _fileOrganizer;
        private FileScanner _fileScanner;

        public ItemActionDialog(FileSystemItem item)
        {
            try
            {
                InitializeComponent();
                _item = item;
                _fileOrganizer = new FileOrganizer();
                _fileScanner = new FileScanner();

                // Show ScanFolder button only if item is a directory
                if (item.IsDirectory && ScanFolderButton != null)
                {
                    ScanFolderButton.Visibility = Visibility.Visible;
                }

                this.DataContext = new { ItemName = item?.Name ?? "Unknown" };
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error initializing item action dialog: {ex.Message}\n\n{ex.StackTrace}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void MoveToRecycleBin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _fileOrganizer.MoveToRecycleBin(_item.FullPath);
                MessageBox.Show($"'{_item.Name}' moved to Recycle Bin.", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error moving file: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetCategory_Click(object sender, RoutedEventArgs e)
        {
            var categoryDialog = new SelectCategoryDialog(_fileOrganizer.GetCategories());
            if (categoryDialog.ShowDialog() == true)
            {
                try
                {
                    _fileOrganizer.SetFileCategory(_item.FullPath, categoryDialog.SelectedCategory);
                    _item.Category = categoryDialog.SelectedCategory;
                    MessageBox.Show($"'{_item.Name}' assigned to category '{categoryDialog.SelectedCategory.Name}'.", 
                        "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Error setting category: {ex.Message}", "Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ScanFolder_Click(object sender, RoutedEventArgs e)
        {
            ScanResultsWindow scanWindow = new ScanResultsWindow(_item.FullPath);
            scanWindow.ShowDialog();
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
