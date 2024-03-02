using AdvancedToDoListMauiApp.Services.Interfaces;
using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AdvancedToDoListMauiApp.Args;

namespace AdvancedToDoListMauiApp.Views;

public partial class RulesPage : ContentPage
{
	public ObservableCollection<UserRule> UserRules { get; set; } = new ObservableCollection<UserRule>();

	private readonly IPunishmentPointService _punishmentPointService = new PunishmentPointService();
	private readonly IUserRuleService _userRuleService = new UserRuleService();
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
		PunishmentPointService.PunishmentPointChanged += UpdatePunishmentPointLabel;
	}
	private void CloseRulePanel()
	{
		RulePanel.IsVisible = false;
		PanelLabelRuleId.Text = string.Empty;
		EntryRuleName.Text = string.Empty;
		EntryPunishmentPoint.Text = string.Empty;
	}
	protected override async void OnAppearing()
	{
		var value = await _punishmentPointService.GetPointValueAsync();

        UpdatePunishmentPointLabel(this, new PunishmentPointValueChangedEventArgs("", value, 0));
        base.OnAppearing();

		CloseRulePanel();
	}
	private async Task UpdateUserRulesCollectionAsync()
	{
		var userRules = await _userRuleService.GetAllUserRulesAsync();

		UserRules.Clear();

		foreach (var rule in userRules)
		{
			UserRules.Add(rule);
		}
	}
	private void UpdatePunishmentPointLabel(object? sender, PunishmentPointValueChangedEventArgs e)
	{
		LabelPunishmentPoints.Text = e.Value.ToString();
	}
	private async void DeleteRule(UserRule userRule)
	{
		var result = await _userRuleService.DeleteUserRuleAsync(userRule);

		if(result > 0)
			UserRules.Remove(userRule);
	}
	private async void ViolatedRule(UserRule userRule)
	{
		int punishPoint = userRule.PunishmentPoint;

		if (punishPoint >= 0)
		{
			await _punishmentPointService.AddValueAsync(punishPoint);
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
	private void ButtonAddNewRule_Clicked(object sender, EventArgs e)
	{
		RulePanel.IsVisible = true;
	}
	private async void InitialCollectionCreation(object? sender, EventArgs e)
	{
		UserRules.Clear();
		BindingContext = this;

		var userRules = await _userRuleService.GetAllUserRulesAsync();

		foreach (var rule in userRules)
			UserRules.Add(rule);
	}
	private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
	{
		//this method don`t do enything
	}
	private void TapBorderClosePanelButton_Tapped(object sender, TappedEventArgs e)
	{
		CloseRulePanel();
	}
	private async void TapBorderSavePanelDataButton_Tapped(object sender, TappedEventArgs e)
	{
		if (!IsPanelRuleDataValid(EntryRuleName.Text, EntryPunishmentPoint.Text))
			return;

		bool ruleExist = !string.IsNullOrEmpty(PanelLabelRuleId.Text);

		int result;

		if (ruleExist) 
		{
			int ruleId = int.Parse(PanelLabelRuleId.Text);

			var userRule = await _userRuleService.GetUserRuleByIdAsync(ruleId);

			if (userRule == null)
			{
                CloseRulePanel();
				return;
            }

			userRule.Name = EntryRuleName.Text;
			userRule.PunishmentPoint = int.Parse(EntryPunishmentPoint.Text);

			result = await _userRuleService.UpdateUserRuleAsync(userRule);
		}
		else
		{
			var newUserRule = new UserRule()
			{
				Name = EntryRuleName.Text,
				PunishmentPoint = int.Parse(EntryPunishmentPoint.Text)
			};

			result = await _userRuleService.AddUserRuleAsync(newUserRule);
		}

		CloseRulePanel();

		if (result > 0)
			await UpdateUserRulesCollectionAsync();
	}
	private static bool IsPanelRuleDataValid(string ruleName, string pp)
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
}