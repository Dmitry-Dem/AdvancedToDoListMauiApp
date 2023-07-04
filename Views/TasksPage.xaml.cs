using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Interfaces;
using AdvancedToDoListMauiApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdvancedToDoListMauiApp.Views;

public partial class TasksPage : ContentPage
{
	public ObservableCollection<TaskGroup> Groups { get; set; } = new ObservableCollection<TaskGroup>();
	public ObservableCollection<UserTask> Tasks { get; set; } = new ObservableCollection<UserTask>();
	private TaskGroup _selectedTaskGroup { get; set; }

	private IUserTaskService _userTaskService = new UserTaskService();
	
	private ITaskGroupService _taskGroupService = new TaskGroupService();
	public ICommand DeleteTaskCommand { get; set; }
	public TasksPage()
	{
		InitializeComponent();

		DeleteTaskCommand = new Command<UserTask>(DeleteUserTask);

		Appearing += InitialCollectionCreation;
	}
	private void UpdateTasksCollection()
	{
		if (Tasks.Count > 0)
		{
			Tasks.Clear();
		}

		if (_selectedTaskGroup != null)
		{
			var sortedTasks = from item in 
						(_selectedTaskGroup.Tasks ?? new List<UserTask>())
						orderby item.IsDone
						select item;

			foreach (var task in sortedTasks)
			{
				Tasks.Add(task);
			}
		}
	}
	private void DeleteUserTask(UserTask task)
	{
		Tasks.Remove(task);

		_userTaskService.DeleteUserTask(task);
	}
	private void OnCheckBoxTapped(object sender, TappedEventArgs e)
	{
		var checkBox = (CheckBox)sender;

		checkBox.IsChecked = !checkBox.IsChecked;

		var userTaskFromList = (UserTask)checkBox.BindingContext;

		var userTaskFromListId = userTaskFromList.Id;

		var userTask = _userTaskService.GetUserTaskById(userTaskFromListId);

		userTask.IsDone = checkBox.IsChecked;

		_userTaskService.UpdateUserTask(userTask);

		if (userTask.IsDone)
		{
			Tasks.Insert(Tasks.Count, userTask);
			Tasks.Remove(userTaskFromList);
		}
		else
		{
			Tasks.Insert(0, userTask);
			Tasks.Remove(userTaskFromList);
		}

	}
	private void ButtonAddNewTask_Clicked(object sender, EventArgs e)
	{
		//open panel to add new task
		if (_selectedTaskGroup != null)
		{
			PanelAddNewUserTask.IsVisible = true;
		}
	}
	private async void InitialCollectionCreation(object sender, EventArgs e)
	{
		BindingContext = this;

		await Task.Delay(300);

		var listGroups = _taskGroupService.GetAllTaskGroups();

		foreach (var item in listGroups)
		{
			await Task.Delay(25);

			Groups.Add(item);
		}

		_selectedTaskGroup = listGroups.First();

		UpdateTasksCollection();

		Appearing -= InitialCollectionCreation;
	}
	private void BorderAddNewGroupButton_Tapped(object sender, TappedEventArgs e)
	{
		PanelAddNewTaskGroup.IsVisible = true;
	}
	private void TapSelectNewCurrentTaskGroup_Tapped(object sender, TappedEventArgs e)
	{
		var taskGroupBorder = sender as Border;

		if (taskGroupBorder != null)
		{
			int taskGroupId = int.Parse(taskGroupBorder.ClassId);

			var selectdetGroup = _taskGroupService.GetTaskGroupById(taskGroupId);

			//Switch Selected Group With First element in Groups collection
			Groups.Move(Groups.IndexOf(Groups.First(g => g.Id == selectdetGroup.Id)), 0);

			CollectionViewGroups.ScrollTo(Groups[0], ScrollToPosition.Start);

			if (selectdetGroup != null)
			{
				_selectedTaskGroup = selectdetGroup;

				UpdateTasksCollection();
			}
		}
	}
	private void BorderDeleteSelectedGroupButto_Tapped(object sender, TappedEventArgs e)
	{
		if (_selectedTaskGroup != null)
		{
			_taskGroupService.DeleteTaskGroup(_selectedTaskGroup);

			Groups.Remove(_selectedTaskGroup);
		}
	}
	//Panel add new UserTask
	private void ClosePanelAddNewUserTask()
	{
		PanelAddNewUserTask.IsVisible = false;

		EntryUserTaskName.Text = string.Empty;
		EntryPunishmentPoint.Text = string.Empty;
	}
	private bool IsPanelDataUserTaskValid(string name, string pp)
	{
		if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(pp))
		{
			try
			{
				int punishmentPoint = int.Parse(pp);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		return false;
	}
	private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
	{

	}
	private void TapBorderClosePanelButton_Tapped(object sender, TappedEventArgs e)
	{
		ClosePanelAddNewUserTask();
	}
	private void TapBorderSavePanelDataUserTaskButton_Tapped(object sender, TappedEventArgs e)
	{
		string userTaskName = EntryUserTaskName.Text;
		string pp = EntryPunishmentPoint.Text;

		if (IsPanelDataUserTaskValid(userTaskName, pp))
		{
			var newTask = new UserTask()
			{
				Description = userTaskName,
				IsDone = false,
				PunishmentPoint = int.Parse(pp),
				TaskGroupId = _selectedTaskGroup.Id
			};

			if (_selectedTaskGroup.Tasks == null)
			{
				_selectedTaskGroup.Tasks = new List<UserTask>();
			}

			_selectedTaskGroup.Tasks.Add(newTask);

			_userTaskService.AddNewUserTask(newTask);

			Tasks.Insert(0, newTask);

			ClosePanelAddNewUserTask();
		}
	}
	//Panel add new TaskGroup
	private void ClosePanelAddNewTaskGroup()
	{
		PanelAddNewTaskGroup.IsVisible = false;

		EntryTaskGroupName.Text = string.Empty;
	}
	private void TapClosePanelTaskGroup_Tapped(object sender, TappedEventArgs e)
	{
		ClosePanelAddNewTaskGroup();
	}
	private void TapBorderSavePanelDataTaskGroupButton_Tapped(object sender, TappedEventArgs e)
	{
		string groupName = EntryTaskGroupName.Text;

		if (!string.IsNullOrEmpty(groupName))
		{
			var newTaskGroup = new TaskGroup()
			{
				Name = groupName,
				Tasks = new List<UserTask>()
			};

			_taskGroupService.AddNewTaskGroup(newTaskGroup);

			Groups.Add(newTaskGroup);

			if (_selectedTaskGroup == null)
				_selectedTaskGroup = newTaskGroup;

			ClosePanelAddNewTaskGroup();
		}
	}
}