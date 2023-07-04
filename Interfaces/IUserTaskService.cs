using AdvancedToDoListMauiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedToDoListMauiApp.Interfaces
{
    public interface IUserTaskService
    {
		List<UserTask> GetAllUserTasks();
		UserTask GetUserTaskById(int Id);
		void AddNewUserTask(UserTask userTask);
		void UpdateUserTask(UserTask userTask);
		void DeleteUserTask(UserTask userTask);
	}
}
