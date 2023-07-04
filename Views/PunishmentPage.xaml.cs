using AdvancedToDoListMauiApp.Interfaces;
using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdvancedToDoListMauiApp.Views;

public partial class PunishmentPage : ContentPage
{
	public ObservableCollection<Punishment> Punishments { get; set; } = new ObservableCollection<Punishment>();

	private IPunishmentPointService _punishmentPointService = new PunishmentPointService();
	private IPunishmentService _punishmentService = new PunishmentService();
	public ICommand OpenPunishmentCommand { get; }
	public PunishmentPage()
	{
		InitializeComponent();

		OpenPunishmentCommand = new Command<Punishment>(DecreasePunishmentValue);

		Appearing += InitialCollectionCreation;
	}
	protected override void OnAppearing()
	{
		UpdatePunishmentPointLabel();

		base.OnAppearing();
	}
	private void UpdatePunishmentCollection()
	{
		var punishList = _punishmentService.GetAllPunishments();

		Punishments.Clear();

		foreach (var punish in punishList)
		{
			Punishments.Add(punish);
		}
	}
	private void UpdatePunishmentPointLabel()
	{
		LabelPunishmentPoints.Text = _punishmentPointService.GetPointValue().ToString();
	}
	private void DecreasePunishmentValue(Punishment punishment)
	{
		var dbPunishment = _punishmentService.GetPunishmentById(punishment.Id);

		if (punishment.ValueDecreaser != dbPunishment.ValueDecreaser)
		{
			dbPunishment.ValueDecreaser = punishment.ValueDecreaser;

			_punishmentService.UpdatePunishment(dbPunishment);
		}

		var updatedPunishment = _punishmentService.DecreasePunishmentValueById(punishment.Id);

		if (updatedPunishment.Value <= 0)
		{
			_punishmentService.DeletePunishment(updatedPunishment);
		}

		//update observable collection
		UpdatePunishmentCollection();
	}
	private async void InitialCollectionCreation(object sender, EventArgs e)
	{
		BindingContext = this;

		await Task.Delay(400);

		var list = _punishmentService.GetAllPunishments();

		foreach (var item in list)
		{
			await Task.Delay(25);

			Punishments.Add(item);
		}
	}
	private void BorderOpenRulesPage_Tapped(object sender, TappedEventArgs e)
	{
		Navigation.PushAsync(new RulesPage());
	}
	private void BorderOpenPointsConverterPage_Tapped(object sender, TappedEventArgs e)
	{
		var newPage = new PointsConverterPage();

		NavigationPage.SetHasNavigationBar(newPage, false);

		Navigation.PushAsync(newPage);
	}
}