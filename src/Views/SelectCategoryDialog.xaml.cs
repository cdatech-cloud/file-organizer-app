using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using FileOrganizerApp.Models;

namespace FileOrganizerApp.Views
{
    public partial class SelectCategoryDialog : Window
    {
        public Category SelectedCategory { get; private set; }

        public SelectCategoryDialog(List<Category> categories)
        {
            InitializeComponent();
            CategoriesListBox.ItemsSource = categories;
        }

        private void CategoriesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CategoriesListBox.SelectedItem is Category category)
            {
                SelectedCategory = category;
                DialogResult = true;
                Close();
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesListBox.SelectedItem is Category category)
            {
                SelectedCategory = category;
                DialogResult = true;
                Close();
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a category.", "Selection Required", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
