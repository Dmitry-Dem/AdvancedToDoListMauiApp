using System.Collections.ObjectModel;
using System.Windows.Input;
using AdvancedToDoListMauiApp.Interfaces;
using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Services;

namespace AdvancedToDoListMauiApp.Views;

public partial class PointsConverterPage : ContentPage
{
	private IPunishmentTypeService _punishmentTypeService = new PunishmentTypeService();
	private IPunishmentPointService _punishmentPointService = new PunishmentPointService();
	private IPunishmentService _punishmentService = new PunishmentService();
	public ICommand DeletePunishmentTypeCommand { get; }
	public ICommand ConvertPunishmentTypeCommand { get; }
	public ObservableCollection<PunishmentType> PunishmentTypes { get; set; }
	public PointsConverterPage()
	{
		InitializeComponent();
		UpdateCollection();

		DeletePunishmentTypeCommand = new Command<PunishmentType>(DeletePunishmentType);
		ConvertPunishmentTypeCommand = new Command<PunishmentType>(ConvertPunishmentPointsToPunishment);

		BindingContext = this;
	}
	protected override void OnAppearing()
	{
		LabelPunishmentPoints.Text = _punishmentPointService.GetPointValue().ToString();

		base.OnAppearing();
	}
	public void ConvertPunishmentPointsToPunishment(PunishmentType punishmentType)
	{
		_punishmentTypeService.UpdatePunishmentType(punishmentType);

		//convert data code:
		var PunishPoint = _punishmentPointService.GetPointValue();

		PunishPoint -= punishmentType.PunishmentPoint;

		if (PunishPoint > 0)
		{
			_punishmentPointService.AddValue(punishmentType.PunishmentPoint * -1);

			var x = _punishmentService
				.GetAllPunishments();

			var dbPunishment = _punishmentService
				.GetAllPunishments()
				.FirstOrDefault(p => p.PunishmentTypeId == punishmentType.Id);

			bool punishmentExist = dbPunishment != null;

			if (punishmentExist)
			{
				dbPunishment.Value += punishmentType.Value;

				_punishmentService.UpdatePunishment(dbPunishment);
			}
			else
			{
				var newPunishment = new Punishment()
				{
					Description = punishmentType.Description,
					Value = punishmentType.Value,
					PunishmentTypeId = punishmentType.Id,
					ValueDecreaser = -1
				};

				_punishmentService.AddNewPunishment(newPunishment);
			}

			LabelPunishmentPoints.Text = _punishmentPointService.GetPointValue().ToString();
		}
	}
	public void DeletePunishmentType(PunishmentType punishmentType)
	{
		_punishmentTypeService.DeletePunishmentType(punishmentType);

		UpdateCollection();
	}
	private void UpdateCollection()
	{
		var punishTypesList = _punishmentTypeService.GetAllPunishmentTypes();

		if (PunishmentTypes == null)
		{
			PunishmentTypes = new ObservableCollection<PunishmentType>();
		}

		PunishmentTypes.Clear();

		foreach (var punType in punishTypesList)
		{
			PunishmentTypes.Add(punType);
		}
	}
	private void ButtonBack_Clicked(object sender, EventArgs e)
	{
		Navigation.PopAsync();
	}
	private void ButtonAddNewPunishmentType_Clicked(object sender, EventArgs e)
	{
		var newPunishmentType = new PunishmentType
		{
			Description = "",
			Value = 0,
			PunishmentPoint = 1
		};

		_punishmentTypeService.AddNewPunishmentType(newPunishmentType);

		UpdateCollection();
	}
}