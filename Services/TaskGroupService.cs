using AdvancedToDoListMauiApp.Services.Interfaces;
using AdvancedToDoListMauiApp.Data;
using AdvancedToDoListMauiApp.Models;

namespace AdvancedToDoListMauiApp.Services
{
    public class TaskGroupService : ITaskGroupService
	{
		private readonly ApplicationDb _db = new();
        public async Task<List<TaskGroup>> GetAllTaskGroupsAsync()
        {
            var list = await _db.GetAllAsync<TaskGroup>();


            foreach (var item in list)
            {
                item.Tasks = (await _db.GetAllAsync<UserTask>())
                    .Where(ut => ut.TaskGroupId == item.Id)
                    .ToList();
            }

            return list;
        }
        public async Task<TaskGroup?> GetTaskGroupByIdAsync(int Id)
        {
            var item = await _db.GetByIdAsync<TaskGroup>(Id);

            if (item == null)
                return default;

            item.Tasks = (await _db.GetAllAsync<UserTask>())
                    .Where(ut => ut.TaskGroupId == item.Id)
                    .ToList();

            return item;
        }
        public async Task<int> AddTaskGroupAsync(TaskGroup item)
        {
            return await _db.AddAsync(item);
        }
        public async Task<int> UpdateTaskGroupAsync(TaskGroup item)
        {
            return await _db.UpdateAsync(item);
        }
        public async Task<int> DeleteTaskGroupAsync(TaskGroup item)
        {
            return await _db.DeleteAsync(item);
        }
    }
}
