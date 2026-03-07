using System.Windows;
using System.Windows.Forms;

namespace FileOrganizerApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
            System.Windows.MessageBox.Show("Category Manager - Not yet implemented", "File Organizer");
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
                    string selectedPath = folderDialog.SelectedPath;
                    ScanResultsWindow scanWindow = new ScanResultsWindow(selectedPath);
                    scanWindow.Show();
                }
            }
        }

        private void ScheduleScan_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Schedule Scan - Not yet implemented", "File Organizer");
        }
    }
}