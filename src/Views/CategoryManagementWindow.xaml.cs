using System.Windows;
using FileOrganizerApp.Services;

namespace FileOrganizerApp.Views
{
    public partial class CategoryManagementWindow : Window
    {
        private CategoryManager _categoryManager;

        public CategoryManagementWindow()
        {
            InitializeComponent();
            _categoryManager = new CategoryManager();
            LoadCategories();
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _categoryManager.GetCategories();
                CategoriesListView.ItemsSource = categories;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var addDialog = new AddCategoryDialog();
            if (addDialog.ShowDialog() == true)
            {
                try
                {
                    _categoryManager.AddCategory(addDialog.CategoryName, addDialog.TargetPath);
                    LoadCategories();
                    MessageBox.Show($"Category '{addDialog.CategoryName}' added successfully.", "Success", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Error adding category: {ex.Message}", "Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EditCategory_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesListView.SelectedItem is not FileOrganizerApp.Models.Category selectedCategory)
            {
                MessageBox.Show("Please select a category to edit.", "Information", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var editDialog = new EditCategoryDialog(selectedCategory);
            if (editDialog.ShowDialog() == true)
            {
                try
                {
                    _categoryManager.UpdateCategory(selectedCategory.Name, editDialog.CategoryName, editDialog.TargetPath);
                    LoadCategories();
                    MessageBox.Show("Category updated successfully.", "Success", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Error updating category: {ex.Message}", "Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesListView.SelectedItem is not FileOrganizerApp.Models.Category selectedCategory)
            {
                MessageBox.Show("Please select a category to delete.", "Information", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to delete category '{selectedCategory.Name}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                _categoryManager.DeleteCategory(selectedCategory.Name);
                LoadCategories();
                MessageBox.Show("Category deleted successfully.", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error deleting category: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
