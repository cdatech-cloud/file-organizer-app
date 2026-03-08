using FileOrganizerApp.Models;

namespace FileOrganizerApp.ViewModels
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }

        public CategoryViewModel(Category category)
        {
            Category = category;
        }

        public string Name => Category.Name;
        public string TargetPath => Category.TargetPath;
    }
}
