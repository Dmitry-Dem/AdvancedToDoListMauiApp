using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedToDoListMauiApp.Interfaces;
using AdvancedToDoListMauiApp.Data;
using AdvancedToDoListMauiApp.Models;

namespace AdvancedToDoListMauiApp.Services
{
    public class UserRuleService : IUserRuleService
	{
		private ApplicationDb _db = new ApplicationDb();
		public List<UserRule> GetAllUserRules()
		{
			return _db.GetAllUserRules();
		}
		public UserRule GetUserRuleById(int Id)
		{
			return _db.GetUserRuleById(Id);
		}
		public void AddNewUserRule(UserRule userRule)
		{
			_db.AddNewUserRule(userRule);
		}
		public void UpdateUserRule(UserRule userRule)
		{
			_db.UpdateUserRule(userRule);
		}
		public void DeleteUserRule(UserRule userRule)
		{
			_db.DeleteUserRule(userRule);
		}
	}
}
