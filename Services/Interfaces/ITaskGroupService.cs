using AdvancedToDoListMauiApp.Models;

namespace AdvancedToDoListMauiApp.Services.Interfaces;

public interface ITaskGroupService
{
    Task<List<TaskGroup>> GetAllTaskGroupsAsync();
    Task<TaskGroup?> GetTaskGroupByIdAsync(int Id);
    Task<int> AddTaskGroupAsync(TaskGroup item);
    Task<int> UpdateTaskGroupAsync(TaskGroup item);
    Task<int> DeleteTaskGroupAsync(TaskGroup item);
}
