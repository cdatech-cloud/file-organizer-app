using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileOrganizerApp.Models;

namespace FileOrganizerApp.Services
{
    public class FileScanner
    {
        public List<FileSystemItem> ScanDirectChildren(string path)
        {
            List<FileSystemItem> items = new List<FileSystemItem>();

            try
            {
                if (!Directory.Exists(path))
                {
                    return items;
                }

                // Get direct child folders
                var directoryInfo = new DirectoryInfo(path);
                var directories = directoryInfo.EnumerateDirectories();
                foreach (var dir in directories)
                {
                    items.Add(new FileSystemItem(dir.Name, dir.FullName, true));
                }

                // Get direct child files
                var files = directoryInfo.EnumerateFiles();
                foreach (var file in files)
                {
                    items.Add(new FileSystemItem(file.Name, file.FullName, false));
                }

                // Sort: folders first, then files, both alphabetically
                items = items.OrderByDescending(x => x.IsDirectory)
                             .ThenBy(x => x.Name)
                             .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error scanning directory {path}: {ex.Message}");
            }

            return items;
        }
    }
}