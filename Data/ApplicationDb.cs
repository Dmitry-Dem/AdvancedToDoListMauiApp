using AdvancedToDoListMauiApp.Models;
using SQLite;

namespace AdvancedToDoListMauiApp.Data
{
	public class ApplicationDb
	{
		private SQLiteConnection _conn;
        public ApplicationDb()
        {
			Init();
        }
        public void Init()
        {
            _conn = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);

            _conn.CreateTable<Punishment>();
            _conn.CreateTable<PunishmentPoint>();
            _conn.CreateTable<PunishmentType>();
            _conn.CreateTable<UserRule>();
            _conn.CreateTable<TaskGroup>();
            _conn.CreateTable<UserTask>();
        }
        //Punishment methods
        public List<Punishment> GetAllPunishments()
		{
            return _conn.Table<Punishment>().ToList();
		}
        public Punishment GetPunishmentById(int Id)
        {
            var punishList = GetAllPunishments();

            return punishList.FirstOrDefault(p => p.Id == Id);
        }
        public void AddNewPunishment(Punishment punishment)
        {
            _conn.Insert(punishment);
        }
        public void UpdatePunishment(Punishment punishment)
        {
            _conn.Update(punishment);
        }
		public void DeletePunishment(Punishment punishment)
		{
			_conn.Delete(punishment);
		}
		//Punishment Point methods
		public List<PunishmentPoint> GetAllPunishmentPoints()
		{
			return _conn.Table<PunishmentPoint>().ToList();
		}
		public void AddNewPunishmentPoint(PunishmentPoint punishmentPoint)
		{
			_conn.Insert(punishmentPoint);
		}
        public void UpdatePunishmentPoint(PunishmentPoint punishmentPoint)
        {
            _conn.Update(punishmentPoint);
        }
		//Punishment Types methods
        public List<PunishmentType> GetAllPunishmentTypes()
        {
            return _conn.Table<PunishmentType>().ToList();
        }
		public PunishmentType GetPunishmentTypeById(int Id)
		{
			var punishTypesList = GetAllPunishmentTypes();

			return punishTypesList.FirstOrDefault(pt => pt.Id == Id);
		}
		public void AddNewPunishmentType(PunishmentType punishmentType)
		{
			_conn.Insert(punishmentType);
		}
		public void UpdatePunishmentType(PunishmentType punishmentType)
		{
			_conn.Update(punishmentType);
		}
		public void DeletePunishmentType(PunishmentType punishmentType)
		{
			_conn.Delete(punishmentType);
		}
		//User Rules methods
		public List<UserRule> GetAllUserRules()
		{
			return _conn.Table<UserRule>().ToList();
		}
		public UserRule GetUserRuleById(int Id)
		{
			var userRulesList = GetAllUserRules();

			return userRulesList.FirstOrDefault(r => r.Id == Id);
		}
		public void AddNewUserRule(UserRule userRule)
		{
			_conn.Insert(userRule);
		}
		public void UpdateUserRule(UserRule userRule)
		{
			_conn.Update(userRule);
		}
		public void DeleteUserRule(UserRule userRule)
		{
			_conn.Delete(userRule);
		}
		//Task Group methods
		public List<TaskGroup> GetAllTaskGroups()
		{
			var taskGroups = _conn.Table<TaskGroup>()
				.ToList();

			foreach (var taskGroup in taskGroups)
			{
				taskGroup.Tasks = GetAllUserTasks()
					.Where(ut => ut.TaskGroupId == taskGroup.Id)
					.ToList();
			}

			return taskGroups;
		}
		public TaskGroup GetTaskGroupById(int Id)
		{
			var taskGroupList = GetAllTaskGroups();

			return taskGroupList.FirstOrDefault(tg => tg.Id == Id);
		}
		public void AddNewTaskGroup(TaskGroup taskGroup)
		{
			_conn.Insert(taskGroup);
		}
		public void UpdateTaskGroup(TaskGroup taskGroup)
		{
			_conn.Update(taskGroup);
		}
		public void DeleteTaskGroup(TaskGroup taskGroup)
		{
			_conn.Delete(taskGroup);
		}
		//User Task methods
		public List<UserTask> GetAllUserTasks()
		{
			return _conn.Table<UserTask>().ToList();
		}
		public UserTask GetUserTaskById(int Id)
		{
			var userTasksList = GetAllUserTasks();

			return userTasksList.FirstOrDefault(u => u.Id == Id);
		}
		public void AddNewUserTask(UserTask userTask)
		{
			_conn.Insert(userTask);
		}
		public void UpdateUserTask(UserTask userTask)
		{
			_conn.Update(userTask);
		}
		public void DeleteUserTask(UserTask userTask)
		{
			_conn.Delete(userTask);
		}
	}
}
