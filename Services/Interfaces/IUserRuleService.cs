using AdvancedToDoListMauiApp.Models;

namespace AdvancedToDoListMauiApp.Services.Interfaces;

public interface IUserRuleService
{
    Task<List<UserRule>> GetAllUserRulesAsync();
    Task<UserRule?> GetUserRuleByIdAsync(int Id);
    Task<int> AddUserRuleAsync(UserRule item);
    Task<int> UpdateUserRuleAsync(UserRule item);
    Task<int> DeleteUserRuleAsync(UserRule item);
}