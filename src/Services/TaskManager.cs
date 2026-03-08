using System;
using System.Collections.Generic;
using System.IO;
using FileOrganizerApp.Models;

namespace FileOrganizerApp.Services
{
    public class TaskManager
    {
        private List<Task> _tasks;
        private FileOrganizer _fileOrganizer;

        public TaskManager()
        {
            _tasks = new List<Task>();
            _fileOrganizer = new FileOrganizer();
        }

        public void AddTask(string action, string subject, string destination = null)
        {
            if (string.IsNullOrWhiteSpace(action))
                throw new ArgumentException("Action cannot be empty.", nameof(action));

            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException("Subject cannot be empty.", nameof(subject));

            if (action.Equals("Move", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(destination))
                throw new ArgumentException("Destination is required for Move action.", nameof(destination));

            var task = new Task(action, subject, destination);
            _tasks.Add(task);
        }

        public List<Task> GetTasks()
        {
            return new List<Task>(_tasks);
        }

        public void DeleteTask(string taskId)
        {
            var task = _tasks.Find(t => t.Id == taskId);
            if (task != null)
            {
                _tasks.Remove(task);
            }
        }

        public void ClearAllTasks()
        {
            _tasks.Clear();
        }

        public void ExecuteTask(Task task)
        {
            if (task.IsCompleted)
                throw new InvalidOperationException("Task already completed.");

            try
            {
                if (task.Action.Equals("Move", StringComparison.OrdinalIgnoreCase))
                {
                    string fileName = Path.GetFileName(task.Subject);
                    string destinationPath = Path.Combine(task.Destination, fileName);

                    if (File.Exists(task.Subject))
                    {
                        if (File.Exists(destinationPath))
                            File.Delete(destinationPath);
                        File.Move(task.Subject, destinationPath);
                    }
                    else if (Directory.Exists(task.Subject))
                    {
                        if (Directory.Exists(destinationPath))
                            Directory.Delete(destinationPath, recursive: true);
                        Directory.Move(task.Subject, destinationPath);
                    }
                    else
                    {
                        throw new FileNotFoundException($"File or folder not found: {task.Subject}");
                    }
                }
                else if (task.Action.Equals("Delete", StringComparison.OrdinalIgnoreCase))
                {
                    _fileOrganizer.MoveToRecycleBin(task.Subject);
                }
                else
                {
                    throw new InvalidOperationException($"Unknown action: {task.Action}");
                }

                task.IsCompleted = true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to execute task: {ex.Message}", ex);
            }
        }

        public void ExecuteAllTasks()
        {
            var incompleteTasks = _tasks.FindAll(t => !t.IsCompleted);
            foreach (var task in incompleteTasks)
            {
                ExecuteTask(task);
            }
        }
    }
}
