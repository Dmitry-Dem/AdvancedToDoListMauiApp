using AdvancedToDoListMauiApp.Services.Interfaces;
using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdvancedToDoListMauiApp.Views;

public partial class TasksPage : ContentPage
{
	public ObservableCollection<TaskGroup> Groups { get; set; } = new ObservableCollection<TaskGroup>();
	public ObservableCollection<UserTask> Tasks { get; set; } = new ObservableCollection<UserTask>();
	private TaskGroup? SelectedTaskGroup { get; set; }

	private readonly IUserTaskService _userTaskService = new UserTaskService();
	
	private readonly ITaskGroupService _taskGroupService = new TaskGroupService();
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

		if (SelectedTaskGroup != null)
		{
			var sortedTasks = from item in 
						(SelectedTaskGroup.Tasks ?? new List<UserTask>())
						orderby item.IsDone
						select item;

			foreach (var task in sortedTasks)
			{
				Tasks.Add(task);
			}
		}
	}
	private async void DeleteUserTask(UserTask task)
	{
		Tasks.Remove(task);

		await _userTaskService.DeleteUserTaskAsync(task);
	}
	private async void OnCheckBoxTapped(object sender, TappedEventArgs e)
	{
		var checkBox = (CheckBox)sender;

		checkBox.IsChecked = !checkBox.IsChecked;

		var userTaskFromList = (UserTask)checkBox.BindingContext;

		var userTaskFromListId = userTaskFromList.Id;

		var userTask = await _userTaskService.GetUserTaskByIdAsync(userTaskFromListId);

		if (userTask == null) return;

		userTask.IsDone = checkBox.IsChecked;

		await _userTaskService.UpdateUserTaskAsync(userTask);

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
		if (SelectedTaskGroup != null)
		{
			PanelAddNewUserTask.IsVisible = true;
		}
	}
	private async void InitialCollectionCreation(object? sender, EventArgs e)
	{
		BindingContext = this;

		var listGroups = await _taskGroupService.GetAllTaskGroupsAsync();

		foreach (var item in listGroups)
		{
			await Task.Delay(25);

			Groups.Add(item);
		}

		SelectedTaskGroup = listGroups.First();

		UpdateTasksCollection();

		Appearing -= InitialCollectionCreation;
	}
	private void BorderAddNewGroupButton_Tapped(object sender, TappedEventArgs e)
	{
		PanelAddNewTaskGroup.IsVisible = true;
	}
	private async void TapSelectNewCurrentTaskGroup_Tapped(object sender, TappedEventArgs e)
	{
		var taskGroupBorder = sender as Border;

		if (taskGroupBorder == null)
			return;
		
		int taskGroupId = int.Parse(taskGroupBorder.ClassId);

		var selectdetGroup = await _taskGroupService.GetTaskGroupByIdAsync(taskGroupId);

		if (selectdetGroup == null) return;

		Groups.Move(Groups.IndexOf(Groups.First(g => g.Id == selectdetGroup.Id)), 0);

		CollectionViewGroups.ScrollTo(Groups[0], ScrollToPosition.Start);

        SelectedTaskGroup = selectdetGroup;

        UpdateTasksCollection();
    }
	private async void BorderDeleteSelectedGroupButto_Tapped(object sender, TappedEventArgs e)
	{
		if (SelectedTaskGroup == null)
			return;

		var result = await _taskGroupService.DeleteTaskGroupAsync(SelectedTaskGroup);

		if (result > 0)
			Groups.Remove(SelectedTaskGroup);
	}
	//Panel add new UserTask
	private void ClosePanelAddNewUserTask()
	{
		PanelAddNewUserTask.IsVisible = false;

		EntryUserTaskName.Text = string.Empty;
		EntryPunishmentPoint.Text = string.Empty;
	}
	private void TapBorderClosePanelButton_Tapped(object sender, TappedEventArgs e)
	{
		ClosePanelAddNewUserTask();
	}
	private async void TapBorderSavePanelDataUserTaskButton_Tapped(object sender, TappedEventArgs e)
	{
		if (SelectedTaskGroup == null)
		{
            ClosePanelAddNewUserTask();
			return;
        }

		string userTaskName = EntryUserTaskName.Text;
		string pp = EntryPunishmentPoint.Text;

		if (!IsPanelDataUserTaskValid(userTaskName, pp))
			return;

		var newTask = new UserTask()
		{
			Description = userTaskName,
			IsDone = false,
			PunishmentPoint = int.Parse(pp),
			TaskGroupId = SelectedTaskGroup.Id
		};

		if (SelectedTaskGroup.Tasks == null)
			SelectedTaskGroup.Tasks = new List<UserTask>();

		SelectedTaskGroup.Tasks.Add(newTask);

		var result = await _userTaskService.AddUserTaskAsync(newTask);

		if (result > 0)
		{
			Tasks.Insert(0, newTask);
		}

		ClosePanelAddNewUserTask();
	}
	private static bool IsPanelDataUserTaskValid(string name, string pp)
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
	private async void TapBorderSavePanelDataTaskGroupButton_Tapped(object sender, TappedEventArgs e)
	{
		string groupName = EntryTaskGroupName.Text;

		if (string.IsNullOrEmpty(groupName))
			return;
		
		var newTaskGroup = new TaskGroup()
		{
			Name = groupName,
			Tasks = new List<UserTask>()
		};

		var result = await _taskGroupService.AddTaskGroupAsync(newTaskGroup);

		if (result > 0)
		{
			Groups.Add(newTaskGroup);

			if (SelectedTaskGroup == null)
				SelectedTaskGroup = newTaskGroup;
		}

		ClosePanelAddNewTaskGroup();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

    }
}