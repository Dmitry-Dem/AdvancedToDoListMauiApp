using AdvancedToDoListMauiApp.Interfaces;
using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdvancedToDoListMauiApp.Views;

public partial class PunishmentPage : ContentPage
{
	private IPunishmentPointService punishPointService = new PunishmentPointService();
	private IPunishmentService punishmentService = new PunishmentService();
	public ICommand OpenPunishmentCommand { get; }
	public ObservableCollection<Punishment> Punishments { get; set; }
	public PunishmentPage()
	{
		InitializeComponent();

		OpenPunishmentCommand = new Command<Punishment>(DecreasePunishmentValue);
	}
	protected override void OnAppearing()
	{
		UpdatePunishmentCollection();

		BindingContext = this;

		LabelPunishmentPoints.Text = punishPointService.GetPointValue().ToString();

		base.OnAppearing();
	}
	private void UpdatePunishmentCollection()
	{
		var punishList = punishmentService.GetAllPunishments();

		if (Punishments == null)
		{
			Punishments = new ObservableCollection<Punishment>();
		}

		Punishments.Clear();

		foreach (var punish in punishList)
		{
			Punishments.Add(punish);
		}
	}
	private void BorderOpenPointsConverterPage_Tapped(object sender, TappedEventArgs e)
	{
		var newPage = new PointsConverterPage();

		NavigationPage.SetHasNavigationBar(newPage, false);

		Navigation.PushAsync(newPage);
	}
	private void BorderOpenRulesPage_Tapped(object sender, TappedEventArgs e)
	{
		Navigation.PushAsync(new RulesPage());
	}
	private void DecreasePunishmentValue(Punishment punishment)
	{
		var dbPunishment = punishmentService.GetPunishmentById(punishment.Id);

		if (punishment.ValueDecreaser != dbPunishment.ValueDecreaser)
		{
			dbPunishment.ValueDecreaser = punishment.ValueDecreaser;

			punishmentService.UpdatePunishment(dbPunishment);
		}

		var updatedPunishment = punishmentService.DecreasePunishmentValueById(punishment.Id);

		if (updatedPunishment.Value <= 0)
		{
			punishmentService.DeletePunishment(updatedPunishment);
		}

		//update observable collection
		UpdatePunishmentCollection();
	}
}