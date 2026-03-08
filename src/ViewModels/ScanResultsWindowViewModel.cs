namespace FileOrganizerApp.ViewModels
{
    public class ScanResultsWindowViewModel
    {
        public string ScannedPath { get; set; }
        public string ItemCount { get; set; }

        public ScanResultsWindowViewModel(string scannedPath, string itemCount)
        {
            ScannedPath = scannedPath;
            ItemCount = itemCount;
        }
    }
}
