namespace FileOrganizerApp.Models
{
    public class ScanTarget
    {
        public string Path { get; set; }
        public bool IsSelected { get; set; }

        public ScanTarget(string path, bool isSelected)
        {
            Path = path;
            IsSelected = isSelected;
        }
    }
}