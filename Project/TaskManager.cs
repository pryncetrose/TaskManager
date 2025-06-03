using System;

namespace TaskManagementApp
{
    class TaskManager
    {
        static void Main()
        {
            TaskFunctions taskFunctions = new TaskFunctions();
            taskFunctions.LoadTasks(); // Load from JSON file

            while (true)
            {
                Console.WriteLine("\n1. Add Task\n2. View Tasks\n3. Search Tasks\n4. Delete Task\n5. Save Tasks\n6. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Task Title: ");
                        string title = Console.ReadLine();

                        Console.Write("Enter Task Description: ");
                        string description = Console.ReadLine();

                        Console.Write("Enter Task Type (Work/Personal): ");
                        string type = Console.ReadLine();

                        BaseTask task = type.ToLower() == "work"
                            ? new WorkTask(0, title, description)
                            : new PersonalTask(0, title, description);

                        taskFunctions.AddTask(task);
                        break;

                    case "2":
                        taskFunctions.ViewTasks();
                        break;

                    case "3":
                        Console.Write("Enter keyword to search: ");
                        string keyword = Console.ReadLine();
                        taskFunctions.SearchTasks(keyword);
                        break;
                    case "4":
                        Console.Write("Enter Task ID/Keyword to delete: ");
                        string deleteKeyword = Console.ReadLine();
                        taskFunctions.DeleteTask(deleteKeyword);

                        break;

                    case "5":
                        taskFunctions.SaveTasks();
                        Console.WriteLine("Tasks saved.");
                        break;

                    case "6":
                        taskFunctions.SaveTasks();
                        Console.WriteLine("Exiting...");
                        return;
                    case "7":
                        Console.Write("Enter Task Type to filter (Work/Personal): ");
                        string filterType = Console.ReadLine();
                        taskFunctions.FilterByType(filterType);
                        break;


                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
    }
}
