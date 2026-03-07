using System;

namespace FileOrganizerApp.Models
{
    public class Category
    {
        public string Name { get; set; }
        public string TargetPath { get; set; }

        public Category(string name, string targetPath)
        {
            Name = name;
            TargetPath = targetPath;
        }
    }
}