using System;
using System.Collections.Generic;
using System.IO;
using FileOrganizerApp.Models;

namespace FileOrganizerApp.Services
{
    public class FileOrganizer
    {
        private List<Category> categories;

        public FileOrganizer()
        {
            categories = new List<Category>();
        }

        public void OrganizeFiles(string sourcePath)
        {
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

        public void CreateCategory(string name, string targetPath)
        {
            var category = new Category(name, targetPath);
            categories.Add(category);
        }

        private bool ShouldMoveFile(string filePath, Category category)
        {
            // Implement logic to determine if the file should be moved to the category
            return true; // Placeholder for actual logic
        }
    }
}