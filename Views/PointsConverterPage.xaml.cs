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
	public ICommand UpdatePunishmentTypeCommand { get; }
	public ObservableCollection<PunishmentType> PunishmentTypes { get; set; } = new ObservableCollection<PunishmentType>();
	public PointsConverterPage()
	{
		InitializeComponent();
		UpdatePunishmentTypesCollection();

		DeletePunishmentTypeCommand = new Command<PunishmentType>(DeletePunishmentType);
		ConvertPunishmentTypeCommand = new Command<PunishmentType>(ConvertPunishmentPointsToPunishment);
		UpdatePunishmentTypeCommand = new Command<PunishmentType>(UpdatePunishmentType);

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

		UpdatePunishmentTypesCollection();
	}
	private void UpdatePunishmentType(PunishmentType punishmentType)
	{
		PanelLabelPunishmentTypeId.Text = punishmentType.Id.ToString();
		EntryPunishmentTypeName.Text = punishmentType.Description;
		EntryPunishmentTypeValue.Text = punishmentType.Value.ToString();
		EntryPunishmentTypePoint.Text = punishmentType.PunishmentPoint.ToString();

		PanelPunishmenType.IsVisible = true;
	}
	private void UpdatePunishmentTypesCollection()
	{
		var punishTypesList = _punishmentTypeService.GetAllPunishmentTypes();

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
		//open panel to add new punType

		PanelPunishmenType.IsVisible = true;

		//var newPunishmentType = new PunishmentType
		//{
		//	Description = "",
		//	Value = 0,
		//	PunishmentPoint = 1
		//};

		//_punishmentTypeService.AddNewPunishmentType(newPunishmentType);
	}
	private void TapBorderClosePanelButton_Tapped(object sender, TappedEventArgs e)
	{
		ClosePunishmentTypePanel();
	}
	private void TapBorderSavePanelDataButton_Tapped(object sender, TappedEventArgs e)
	{
		if (IsPanelPunishmentTypeDataValid(EntryPunishmentTypeName.Text, EntryPunishmentTypeValue.Text, EntryPunishmentTypePoint.Text))
		{
			//check if we shoud add new PunishType or update existed
			bool punishmentType = !string.IsNullOrEmpty(PanelLabelPunishmentTypeId.Text);

			if (punishmentType)  //update PunishType
			{
				int punishTypeId = int.Parse(PanelLabelPunishmentTypeId.Text);

				var punishType = _punishmentTypeService.GetPunishmentTypeById(punishTypeId);

				punishType.Description = EntryPunishmentTypeName.Text;
				punishType.Value = int.Parse(EntryPunishmentTypeValue.Text);
				punishType.PunishmentPoint = int.Parse(EntryPunishmentTypePoint.Text);

				_punishmentTypeService.UpdatePunishmentType(punishType);
			}
			else  //create and add new PunishType
			{
				var newPunishType = new PunishmentType()
				{
					Description = EntryPunishmentTypeName.Text,
					Value = int.Parse(EntryPunishmentTypeValue.Text),
					PunishmentPoint = int.Parse(EntryPunishmentTypePoint.Text)
				};

				_punishmentTypeService.AddNewPunishmentType(newPunishType);
			}
			//close panel
			ClosePunishmentTypePanel();

			//update Collection of rules:
			UpdatePunishmentTypesCollection();
		}

	}
	private bool IsPanelPunishmentTypeDataValid(string description,  string value, string pp)
	{
		if (!string.IsNullOrEmpty(description) && !string.IsNullOrEmpty(pp) && !string.IsNullOrEmpty(value))
		{
			try
			{
				int punishmentPoint = int.Parse(pp);
				int _value = int.Parse(value);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		return false;
	}
	private void ClosePunishmentTypePanel()
	{
		PanelPunishmenType.IsVisible = false;
		PanelLabelPunishmentTypeId.Text = string.Empty;
		EntryPunishmentTypeName.Text = string.Empty;
		EntryPunishmentTypeValue.Text = string.Empty;
		EntryPunishmentTypePoint.Text = string.Empty;
	}
	private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
	{

	}
}