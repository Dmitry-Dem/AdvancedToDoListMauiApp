using AdvancedToDoListMauiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedToDoListMauiApp.Interfaces
{
    public interface ITaskGroupService
    {
		List<TaskGroup> GetAllTaskGroups();
		TaskGroup GetTaskGroupById(int Id);
		void AddNewTaskGroup(TaskGroup taskGroup);
		void UpdateTaskGroup(TaskGroup taskGroup);
		void DeleteTaskGroup(TaskGroup taskGroup);
	}
}
