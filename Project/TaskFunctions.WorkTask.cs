using System.Text.Json.Serialization;

namespace TaskManagementApp
{
    public class WorkTask : BaseTask
    {
        public WorkTask() : base() { }
        public WorkTask(int id, string title, string description)
            : base(id, title, description, "Work") { }

        public override void DisplayTaskType()
        {
            Console.WriteLine("This is a Work Task.");
        }
    }
}