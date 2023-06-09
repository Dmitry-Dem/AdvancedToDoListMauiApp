using AdvancedToDoListMauiApp.Interfaces;
using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Services;
using System.Collections.ObjectModel;

namespace AdvancedToDoListMauiApp.Views;

public partial class PunishmentPage : ContentPage
{
	private IPunishmentPointService punishPointService = new PunishmentPointService();
	private IPunishmentService punishmentService = new PunishmentService();
	public ObservableCollection<string> Punishments { get; set; }
	public PunishmentPage()
	{
		InitializeComponent();

		Punishments = new ObservableCollection<string>()
		{
			"HI",
			"jbchjd"
		};

		BindingContext = this;

		LabelPunishmentPoints.Text = punishPointService.GetPointValue().ToString();
	}
	private void LoadData()
	{
		//var dbPunishments = punishmentService.GetAllPunishments();

		//if (dbPunishments == null)
		//{
		//	Punishments.Add(
		//		new Punishment
		//		{
		//			Id = 0,
		//			Description = "Db is empty!",
		//			Value = 0
		//		});
		//}
		//else
		//{
		//	foreach (var punish in dbPunishments)
		//	{
		//		Punishments.Add(punish);
		//	}
		//}
	}
	private void BorderOpenPointsConverterPage_Tapped(object sender, TappedEventArgs e)
	{
		Navigation.PushAsync(new PointsConverterPage());
	}
	private void BorderOpenRulesPage_Tapped(object sender, TappedEventArgs e)
	{
		Navigation.PushAsync(new RulesPage());
	}
}