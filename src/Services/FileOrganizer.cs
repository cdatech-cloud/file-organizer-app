using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileOrganizerApp.Models;

namespace FileOrganizerApp.Services
{
    public class FileOrganizer
    {
        private CategoryManager _categoryManager;
        private Dictionary<string, Category> fileCategories;

        public FileOrganizer()
        {
            _categoryManager = new CategoryManager();
            fileCategories = new Dictionary<string, Category>();
        }

        public void OrganizeFiles(string sourcePath)
        {
            var categories = _categoryManager.GetCategories();
            foreach (var category in categories)
            {
                string targetPath = category.TargetPath;

                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }

                var files = Directory.GetFiles(sourcePath);

                foreach (var file in files)
                {
                    if (ShouldMoveFile(file, category))
                    {
                        string fileName = Path.GetFileName(file);
                        string destinationFile = Path.Combine(targetPath, fileName);
                        File.Move(file, destinationFile);
                    }
                }
            }
        }

        public List<Category> GetCategories()
        {
            return _categoryManager.GetCategories();
        }

        public void MoveToRecycleBin(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(filePath, 
                        Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, 
                        Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                }
                else if (Directory.Exists(filePath))
                {
                    Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(filePath, 
                        Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, 
                        Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                }
                else
                {
                    throw new FileNotFoundException($"File or folder not found: {filePath}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to move to recycle bin: {ex.Message}", ex);
            }
        }

        public void SetFileCategory(string filePath, Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            fileCategories[filePath] = category;
        }

        public Category GetFileCategory(string filePath)
        {
            return fileCategories.ContainsKey(filePath) ? fileCategories[filePath] : null;
        }

        private bool ShouldMoveFile(string filePath, Category category)
        {
            // Implement logic to determine if the file should be moved to the category
            return true; // Placeholder for actual logic
        }
    }
}

    }
}