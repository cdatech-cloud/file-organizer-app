namespace FileOrganizerApp.Models
{
    public class FileSystemItem
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool IsDirectory { get; set; }
        public string Type => IsDirectory ? "Folder" : "File";

        public FileSystemItem(string name, string fullPath, bool isDirectory)
        {
            Name = name;
            FullPath = fullPath;
            IsDirectory = isDirectory;
        }
    }
}
