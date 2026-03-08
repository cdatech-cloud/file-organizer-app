using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FileOrganizerApp.Models;

namespace FileOrganizerApp.Services
{
    public class CategoryManager
    {
        private List<Category> _categories;
        private readonly string _categoriesFilePath;

        public CategoryManager()
        {
            _categories = new List<Category>();
            _categoriesFilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "FileOrganizerApp",
                "categories.json");

            LoadCategories();
        }

        public void AddCategory(string name, string targetPath)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(targetPath))
                throw new ArgumentException("Target path cannot be empty.", nameof(targetPath));

            if (_categories.Any(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Category '{name}' already exists.");

            var category = new Category(name, targetPath);
            _categories.Add(category);
            SaveCategories();
        }

        public void UpdateCategory(string oldName, string newName, string newTargetPath)
        {
            var category = _categories.FirstOrDefault(c => c.Name.Equals(oldName, StringComparison.OrdinalIgnoreCase));
            if (category == null)
                throw new InvalidOperationException($"Category '{oldName}' not found.");

            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Category name cannot be empty.", nameof(newName));

            if (string.IsNullOrWhiteSpace(newTargetPath))
                throw new ArgumentException("Target path cannot be empty.", nameof(newTargetPath));

            if (!newName.Equals(oldName, StringComparison.OrdinalIgnoreCase) &&
                _categories.Any(c => c.Name.Equals(newName, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Category '{newName}' already exists.");

            category.Name = newName;
            category.TargetPath = newTargetPath;
            SaveCategories();
        }

        public void DeleteCategory(string name)
        {
            var category = _categories.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (category == null)
                throw new InvalidOperationException($"Category '{name}' not found.");

            _categories.Remove(category);
            SaveCategories();
        }

        public List<Category> GetCategories()
        {
            return new List<Category>(_categories);
        }

        public Category GetCategory(string name)
        {
            return _categories.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        private void SaveCategories()
        {
            try
            {
                var directory = Path.GetDirectoryName(_categoriesFilePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(_categories, options);
                File.WriteAllText(_categoriesFilePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save categories: {ex.Message}", ex);
            }
        }

        private void LoadCategories()
        {
            try
            {
                if (File.Exists(_categoriesFilePath))
                {
                    var json = File.ReadAllText(_categoriesFilePath);
                    _categories = JsonSerializer.Deserialize<List<Category>>(json) ?? new List<Category>();
                }
                else
                {
                    _categories = new List<Category>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading categories: {ex.Message}");
                _categories = new List<Category>();
            }
        }
    }
}
