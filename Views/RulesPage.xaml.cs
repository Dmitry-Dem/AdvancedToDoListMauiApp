using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Interfaces;
using AdvancedToDoListMauiApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdvancedToDoListMauiApp.Views;

public partial class RulesPage : ContentPage
{
	public ObservableCollection<UserRule> UserRules { get; set; } = new ObservableCollection<UserRule>();

	private IPunishmentPointService _punishmentPointService = new PunishmentPointService();
	private IUserRuleService _userRuleService = new UserRuleService();
	public ICommand DeleteRuleCommand { get; set; }
	public ICommand UpdateRuleCommand { get; set; }
	public ICommand ViolatedRuleCommand { get; set; }
	public RulesPage()
	{
		InitializeComponent();

		DeleteRuleCommand = new Command<UserRule>(DeleteRule);
		ViolatedRuleCommand = new Command<UserRule>(ViolatedRule);
		UpdateRuleCommand = new Command<UserRule>(UpdateRule);

		Appearing += InitialCollectionCreation;
	}
	private void CloseRulePanel()
	{
		RulePanel.IsVisible = false;
		PanelLabelRuleId.Text = string.Empty;
		EntryRuleName.Text = string.Empty;
		EntryPunishmentPoint.Text = string.Empty;
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();

		CloseRulePanel();
		UpdatePunishmentPointLabel();
	}
	private void UpdateUserRulesCollection()
	{
        var userRules = _userRuleService.GetAllUserRules();

		UserRules.Clear();

		foreach (var rule in userRules)
		{
			UserRules.Add(rule);
		}
	}
	private void UpdatePunishmentPointLabel()
	{
		LabelPunishmentPoints.Text = _punishmentPointService.GetPointValue().ToString();
	}
	private void DeleteRule(UserRule userRule)
	{
		_userRuleService.DeleteUserRule(userRule);

		UserRules.Remove(userRule);
	}
	private void ViolatedRule(UserRule userRule)
	{
		int punishPoint = userRule.PunishmentPoint;

		if (punishPoint >= 0)
		{
			_punishmentPointService.AddValue(punishPoint);

			UpdatePunishmentPointLabel();
		}
	}
	private void UpdateRule(UserRule userRule)
	{
		//Open Rule Panel:
		RulePanel.IsVisible = true;

		//Set Data
		PanelLabelRuleId.Text = userRule.Id.ToString();
		EntryRuleName.Text = userRule.Name;
		EntryPunishmentPoint.Text = userRule.PunishmentPoint.ToString();
	}
	private void ButtonBack_Clicked(object sender, EventArgs e)
	{
		Navigation.PopAsync();
    }
	private bool IsPanelRuleDataValid(string ruleName, string pp)
	{
		if (!string.IsNullOrEmpty(ruleName) && !string.IsNullOrEmpty(pp))
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
	private void ButtonAddNewRule_Clicked(object sender, EventArgs e)
	{
		RulePanel.IsVisible = true;
	}
	private async void InitialCollectionCreation(object sender, EventArgs e)
	{
		UserRules.Clear();
		BindingContext = this;

		await Task.Delay(600);

		var userRules = _userRuleService.GetAllUserRules();

		foreach (var rule in userRules)
		{
			await Task.Delay(50);

			UserRules.Add(rule);
		}
	}
	private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
	{
		//this method don`t do enything
	}
	private void TapBorderClosePanelButton_Tapped(object sender, TappedEventArgs e)
	{
		CloseRulePanel();
	}
	private void TapBorderSavePanelDataButton_Tapped(object sender, TappedEventArgs e)
	{
		if (IsPanelRuleDataValid(EntryRuleName.Text, EntryPunishmentPoint.Text))
		{
			//check if we shoud add new rule or update existed
			bool ruleExist = !string.IsNullOrEmpty(PanelLabelRuleId.Text);

			if (ruleExist)  //update rule
			{
				int ruleId = int.Parse(PanelLabelRuleId.Text);

				var userRule = _userRuleService.GetUserRuleById(ruleId);

				userRule.Name = EntryRuleName.Text;
				userRule.PunishmentPoint = int.Parse(EntryPunishmentPoint.Text);

				_userRuleService.UpdateUserRule(userRule);
			}
			else  //create and add new rule
			{
				var newUserRule = new UserRule()
				{
					Name = EntryRuleName.Text,
					PunishmentPoint = int.Parse(EntryPunishmentPoint.Text)
				};

				_userRuleService.AddNewUserRule(newUserRule);
			}
			//close panel
			CloseRulePanel();

			//update Collection of rules:
			UpdateUserRulesCollection();
		}
	}
}