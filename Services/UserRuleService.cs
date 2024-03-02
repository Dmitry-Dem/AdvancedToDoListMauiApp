using AdvancedToDoListMauiApp.Services.Interfaces;
using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Data;

namespace AdvancedToDoListMauiApp.Services
{
    public class UserRuleService : IUserRuleService
	{
		private readonly ApplicationDb _db = new();
        public async Task<List<UserRule>> GetAllUserRulesAsync()
        {
            return await _db.GetAllAsync<UserRule>();
        }
        public async Task<UserRule?> GetUserRuleByIdAsync(int Id)
        {
            return await _db.GetByIdAsync<UserRule>(Id);
        }
        public async Task<int> AddUserRuleAsync(UserRule item)
        {
            return await _db.AddAsync(item);
        }
        public async Task<int> UpdateUserRuleAsync(UserRule item)
        {
            return await _db.UpdateAsync(item);
        }
        public async Task<int> DeleteUserRuleAsync(UserRule item)
        {
            return await _db.DeleteAsync(item);
        }
    }
}
