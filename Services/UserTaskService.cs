using AdvancedToDoListMauiApp.Services.Interfaces;
using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Data;

namespace AdvancedToDoListMauiApp.Services
{
    public class UserTaskService : IUserTaskService
    {
		private readonly ApplicationDb _db = new();
        public async Task<List<UserTask>> GetAllUserTasksAsync()
        {
            return await _db.GetAllAsync<UserTask>();
        }
        public async Task<UserTask?> GetUserTaskByIdAsync(int Id)
        {
            return await _db.GetByIdAsync<UserTask>(Id);
        }
        public async Task<int> AddUserTaskAsync(UserTask item)
        {
            return await _db.AddAsync(item);
        }
        public async Task<int> UpdateUserTaskAsync(UserTask item)
        {
            return await _db.UpdateAsync(item);
        }
        public async Task<int> DeleteUserTaskAsync(UserTask item)
        {
            return await _db.DeleteAsync(item);
        }
    }
}
