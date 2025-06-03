using System.Text.Json.Serialization;

namespace TaskManagementApp
{
    public class PersonalTask : BaseTask
    {
        public PersonalTask() : base() { }
        public PersonalTask(int id, string title, string description)
            : base(id, title, description, "Personal") { }

        public override void DisplayTaskType()
        {
            Console.WriteLine("This is a Personal Task.");
        }

    }
}