using AdvancedToDoListMauiApp.Models;

namespace AdvancedToDoListMauiApp.Interfaces
{
	public interface IUserRuleService
	{
		List<UserRule> GetAllUserRules();
		UserRule GetUserRuleById(int Id);
		void AddNewUserRule(UserRule userRule);
		void UpdateUserRule(UserRule userRule);
		void DeleteUserRule(UserRule userRule);
	}
}