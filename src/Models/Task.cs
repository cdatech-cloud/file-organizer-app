using System;

namespace FileOrganizerApp.Models
{
    public class Task
    {
        public string Id { get; set; }
        public string Action { get; set; } // "Move" or "Delete"
        public string Subject { get; set; } // Absolute path of file/folder
        public string Destination { get; set; } // Target location (only for Move action)
        public DateTime CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
        public string Status { get; set; } // "Todo", "InProgress", "Completed"

        public Task()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            IsCompleted = false;
            Status = "Todo";
        }

        public Task(string action, string subject, string destination = null)
        {
            Id = Guid.NewGuid().ToString();
            Action = action;
            Subject = subject;
            Destination = destination;
            CreatedAt = DateTime.Now;
            IsCompleted = false;
            Status = "Todo";
        }

        public string DisplayName => System.IO.Path.GetFileName(Subject);
    }
}
