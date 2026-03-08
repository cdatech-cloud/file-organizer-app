namespace FileOrganizerApp.ViewModels
{
    public class ItemActionDialogViewModel
    {
        public string ItemName { get; set; }

        public ItemActionDialogViewModel(string itemName)
        {
            ItemName = itemName;
        }
    }
}
