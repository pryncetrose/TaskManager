using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagementApp;
using System.Threading.Tasks;
using System.Collections;

namespace TaskManagementApp
{
    public interface ITaskManager
    {
        Task AddTaskAsync(BaseTask task);
        IAsyncEnumerable<BaseTask> ViewTasksAsync();
        Task SaveTasksAsync();
        Task LoadTasksAsync();
    }
}


