using System.Windows;
using System.Windows.Forms;
using FileOrganizerApp.Models;

namespace FileOrganizerApp.Views
{
    public partial class EditCategoryDialog : Window
    {
        public string CategoryName { get; private set; }
        public string TargetPath { get; private set; }

        public EditCategoryDialog(Category category)
        {
            InitializeComponent();
            CategoryNameTextBox.Text = category.Name;
            TargetPathTextBox.Text = category.TargetPath;
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select the target location for this category";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    TargetPathTextBox.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CategoryNameTextBox.Text))
            {
                System.Windows.MessageBox.Show("Please enter a category name.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TargetPathTextBox.Text))
            {
                System.Windows.MessageBox.Show("Please enter or select a target path.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CategoryName = CategoryNameTextBox.Text.Trim();
            TargetPath = TargetPathTextBox.Text.Trim();
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
