using FileOrganizerApp.Services;

namespace FileOrganizerApp
{
    public static class AppContext
    {
        public static TaskManager TaskManager { get; private set; }

        static AppContext()
        {
            TaskManager = new TaskManager();
        }
    }
}
