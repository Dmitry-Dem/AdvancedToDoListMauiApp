using AdvancedToDoListMauiApp.Services.Interfaces;
using AdvancedToDoListMauiApp.Args;
using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AdvancedToDoListMauiApp.Views;

public partial class PunishmentPage : ContentPage
{
	public ObservableCollection<Punishment> Punishments { get; set; } = new ObservableCollection<Punishment>();

	private readonly IPunishmentPointService _punishmentPointService = new PunishmentPointService();
	private readonly IPunishmentService _punishmentService = new PunishmentService();
    public ICommand OpenPunishmentCommand { get; }
    public PunishmentPage()
	{
        InitializeComponent();

		OpenPunishmentCommand = new Command<Punishment>(DecreasePunishmentValue);

		Appearing += InitialCollectionCreation;
		PunishmentPointService.PunishmentPointChanged += UpdatePunishmentPointLabel;
	}
	protected override async void OnAppearing()
	{
		var value = await _punishmentPointService.GetPointValueAsync();

        UpdatePunishmentPointLabel(this, new PunishmentPointValueChangedEventArgs("", value, 0));

        base.OnAppearing();
	}
	private async Task UpdatePunishmentCollectionAsync()
	{
		var punishList = await _punishmentService.GetAllPunishmentsAsync();

        Punishments.Clear();

		foreach (var punish in punishList)
			Punishments.Add(punish);
	}
	private async void DecreasePunishmentValue(Punishment punishment)
	{
		var dbPunishment = await _punishmentService.GetPunishmentByIdAsync(punishment.Id);

		if (dbPunishment == null) return;

		if (punishment.ValueDecreaser != dbPunishment.ValueDecreaser)
		{
			dbPunishment.ValueDecreaser = punishment.ValueDecreaser;

			await _punishmentService.UpdatePunishmentAsync(dbPunishment);
		}

		var updatedPunishment = await _punishmentService.DecreasePunishmentValueByIdAsync(punishment.Id);

		if (updatedPunishment == null) return;

		if (updatedPunishment.Value <= 0)
			await _punishmentService.DeletePunishmentAsync(updatedPunishment);

		await UpdatePunishmentCollectionAsync();
	}
	private async void InitialCollectionCreation(object? sender, EventArgs e)
	{
		Punishments.Clear();
		BindingContext = this;

		var list = await _punishmentService.GetAllPunishmentsAsync();

		foreach (var item in list)
			Punishments.Add(item);
	}
	private void BorderOpenRulesPage_Tapped(object sender, TappedEventArgs e)
	{
		Navigation.PushAsync(new RulesPage());
	}
	private void BorderOpenPointsConverterPage_Tapped(object sender, TappedEventArgs e)
	{
		Navigation.PushAsync(new PointsConverterPage());
	}
	private void UpdatePunishmentPointLabel(object? sender, PunishmentPointValueChangedEventArgs e)
	{
		LabelPunishmentPoints.Text = e.Value.ToString();
	}
}