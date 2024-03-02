using AdvancedToDoListMauiApp.Models;

namespace AdvancedToDoListMauiApp.Services.Interfaces;

public interface IUserTaskService
{
    Task<List<UserTask>> GetAllUserTasksAsync();
    Task<UserTask?> GetUserTaskByIdAsync(int Id);
    Task<int> AddUserTaskAsync(UserTask item);
    Task<int> UpdateUserTaskAsync(UserTask item);
    Task<int> DeleteUserTaskAsync(UserTask item);
}
