using System.Windows;
using FileOrganizerApp.Services;

namespace FileOrganizerApp.Views
{
    public partial class TasksWindow : Window
    {
        private TaskManager _taskManager;

        public TasksWindow()
        {
            InitializeComponent();
            _taskManager = new TaskManager();
            LoadTasks();
        }

        public TasksWindow(TaskManager taskManager)
        {
            InitializeComponent();
            _taskManager = taskManager;
            LoadTasks();
        }

        private void LoadTasks()
        {
            try
            {
                var tasks = _taskManager.GetTasks();
                TasksListView.ItemsSource = tasks;
                
                if (tasks.Count == 0)
                {
                    System.Windows.MessageBox.Show("No pending tasks.", "Information", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show($"Error loading tasks: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteAll_Click(object sender, RoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show(
                "Execute all pending tasks?",
                "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                _taskManager.ExecuteAllTasks();
                LoadTasks();
                System.Windows.MessageBox.Show("All tasks executed successfully.", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show($"Error executing tasks: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TasksListView.SelectedItem is not FileOrganizerApp.Models.Task selectedTask)
            {
                System.Windows.MessageBox.Show("Please select a task to delete.", "Information", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = System.Windows.MessageBox.Show(
                $"Delete task for '{selectedTask.DisplayName}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                _taskManager.DeleteTask(selectedTask.Id);
                LoadTasks();
                System.Windows.MessageBox.Show("Task deleted.", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show($"Error deleting task: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show(
                "Clear all pending tasks?",
                "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                _taskManager.ClearAllTasks();
                LoadTasks();
                System.Windows.MessageBox.Show("All tasks cleared.", "Success", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show($"Error clearing tasks: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
