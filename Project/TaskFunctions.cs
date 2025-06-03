using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using TaskManagementApp;

namespace TaskManagementApp
{
    public class TaskFunctions
    {
        private List<BaseTask> tasks = new();
        private const string FilePath = "tasks.json";
        private int nextId = 1;

        // Add a new task and save to file
        public void AddTask(BaseTask task)
        {
            task.Id = nextId++;
            tasks.Add(task);
            Console.WriteLine($"Task added successfully with ID: {task.Id}");
            SaveTasks();
        }

        // View all tasks using LINQ
        public void ViewTasks()
        {
            if (!tasks.Any())
            {
                Console.WriteLine("No tasks available.");
                return;
            }

            foreach (var task in tasks.OrderBy(t => t.Id)) // LINQ: sort by ID
            {
                Console.WriteLine($"{task.Id}: {task.Title} - {task.Description} ({task.TaskType})");
            }
        }

        // Search tasks by keyword using LINQ
        public void SearchTasks(string keyword)
        {
            var results = tasks.Where(t =>
                t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                t.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase));

            if (!results.Any())
            {
                Console.WriteLine("No matching tasks found.");
                return;
            }

            foreach (var task in results)
            {
                Console.WriteLine($"{task.Id}: {task.Title} - {task.Description} ({task.TaskType})");
            }
        }

        public void DeleteTask(string keyword)
        {
            var taskToDelete = tasks.FirstOrDefault(t => t.Id.ToString() == keyword || t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            if (taskToDelete != null)
            {
                tasks.Remove(taskToDelete);
                Console.WriteLine($"Task with ID {taskToDelete.Id} deleted successfully.");
                SaveTasks();
            }
            else
            {
                Console.WriteLine("Task not found.");
            }

        }

        // Save tasks to a JSON file
        public void SaveTasks()
        {
            try
            {
                string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving tasks: {ex.Message}");
            }
        }
        // View only tasks of a specific type using LINQ
        public void FilterByType(string type)
        {
            var filtered = tasks
                .Where(t => t.TaskType.Equals(type, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!filtered.Any())
            {
                Console.WriteLine($"No {type} tasks found.");
                return;
            }

            foreach (var task in filtered)
            {
                Console.WriteLine($"{task.Id}: {task.Title} - {task.Description} ({task.TaskType})");
            }
        }

        // Load tasks from JSON file
        public void LoadTasks()
        {
            if (!File.Exists(FilePath))
            {
                tasks = new List<BaseTask>();
                return;
            }

            try
            {
                string json = File.ReadAllText(FilePath);
                tasks = JsonSerializer.Deserialize<List<BaseTask>>(json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        Converters = { new TaskConverter() }
                    }) ?? new List<BaseTask>();

                // Update nextId to avoid duplicate IDs
                if (tasks.Any())
                    nextId = tasks.Max(t => t.Id) + 1;

                Console.WriteLine("Tasks loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading tasks: {ex.Message}");
                tasks = new List<BaseTask>();
            }

        }
        // Internal method for unit testing search functionality (not used in UI)
        public IEnumerable<BaseTask> SearchInternal(string keyword)
        {
            return tasks.Where(t =>
                t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                t.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

    }
}

