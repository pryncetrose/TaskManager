using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace TaskManagementApp
{
    // Custom task class inheriting from BaseTask
    public class CustomTask : BaseTask 
    {
        // Constructor for CustomTask
        public CustomTask(int id, string title, string description, string taskType)
            : base(id, title, description, taskType) { }

        // Default constructor for EF Core
        public override void DisplayTaskType()
        {
            Console.WriteLine($"Task Type: {TaskType}");
        }
    }
}