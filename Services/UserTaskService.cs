using AdvancedToDoListMauiApp.Data;
using AdvancedToDoListMauiApp.Interfaces;
using AdvancedToDoListMauiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedToDoListMauiApp.Services
{
    public class UserTaskService : IUserTaskService
    {
		private ApplicationDb _db = new ApplicationDb();
		public List<UserTask> GetAllUserTasks()
		{
			return _db.GetAllUserTasks();
		}
		public UserTask GetUserTaskById(int Id)
		{
			return _db.GetUserTaskById(Id);
		}
		public void AddNewUserTask(UserTask userTask)
		{
			_db.AddNewUserTask(userTask);
		}
		public void UpdateUserTask(UserTask userTask)
		{
			_db.UpdateUserTask(userTask);
		}
		public void DeleteUserTask(UserTask userTask)
		{
			_db.DeleteUserTask(userTask);
		}
	}
}
