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
    public class TaskGroupService : ITaskGroupService
	{
		private ApplicationDb _db = new ApplicationDb();
		public List<TaskGroup> GetAllTaskGroups()
		{
			return _db.GetAllTaskGroups();
		}
		public TaskGroup GetTaskGroupById(int Id)
		{
			return _db.GetTaskGroupById(Id);
		}
		public void AddNewTaskGroup(TaskGroup taskGroup)
		{
			_db.AddNewTaskGroup(taskGroup);
		}
		public void UpdateTaskGroup(TaskGroup taskGroup)
		{
			_db.UpdateTaskGroup(taskGroup);
		}
		public void DeleteTaskGroup(TaskGroup taskGroup)
		{
			_db.DeleteTaskGroup(taskGroup);
		}
	}
}
