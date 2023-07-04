using System.Collections.ObjectModel;
using System.Windows.Input;
using AdvancedToDoListMauiApp.Args;
using AdvancedToDoListMauiApp.Interfaces;
using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Services;

namespace AdvancedToDoListMauiApp.Views;

public partial class PointsConverterPage : ContentPage
{
	public ObservableCollection<PunishmentType> PunishmentTypes { get; set; } = new ObservableCollection<PunishmentType>();

	private IPunishmentService _punishmentService = new PunishmentService();
	private IPunishmentTypeService _punishmentTypeService = new PunishmentTypeService();
	private IPunishmentPointService _punishmentPointService = new PunishmentPointService();
	public ICommand DeletePunishmentTypeCommand { get; }
	public ICommand UpdatePunishmentTypeCommand { get; }
	public ICommand ConvertPunishmentTypeCommand { get; }
	public PointsConverterPage()
	{
		InitializeComponent();
		UpdatePunishmentPointLabel(this, new PunishmentPointValueChangedEventArgs("", _punishmentPointService.GetPointValue()));

		DeletePunishmentTypeCommand = new Command<PunishmentType>(DeletePunishmentType);
		ConvertPunishmentTypeCommand = new Command<PunishmentType>(ConvertPunishmentPointsToPunishment);
		UpdatePunishmentTypeCommand = new Command<PunishmentType>(UpdatePunishmentType);

		Appearing += InitialCollectionCreation;
		PunishmentPointService.PunishmentPointChanged += UpdatePunishmentPointLabel;
	}
	private void ClosePunishmentTypePanel()
	{
		PanelPunishmenType.IsVisible = false;
		PanelLabelPunishmentTypeId.Text = string.Empty;
		EntryPunishmentTypeName.Text = string.Empty;
		EntryPunishmentTypeValue.Text = string.Empty;
		EntryPunishmentTypePoint.Text = string.Empty;
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
	public void DeletePunishmentType(PunishmentType punishmentType)
	{
		_punishmentTypeService.DeletePunishmentType(punishmentType);

		PunishmentTypes.Remove(punishmentType);
	}
	private void UpdatePunishmentType(PunishmentType punishmentType)
	{
		PanelLabelPunishmentTypeId.Text = punishmentType.Id.ToString();
		EntryPunishmentTypeName.Text = punishmentType.Description;
		EntryPunishmentTypeValue.Text = punishmentType.Value.ToString();
		EntryPunishmentTypePoint.Text = punishmentType.PunishmentPoint.ToString();

		PanelPunishmenType.IsVisible = true;
	}
	private async void InitialCollectionCreation(object sender, EventArgs e)
	{
		PunishmentTypes.Clear();
		BindingContext = this;

		await Task.Delay(200);

		var list = _punishmentTypeService.GetAllPunishmentTypes();

		foreach (var item in list)
		{
			await Task.Delay(25);

			PunishmentTypes.Add(item);
		}
	}
	private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
	{

	}
	private void ButtonAddNewPunishmentType_Clicked(object sender, EventArgs e)
	{
		PanelPunishmenType.IsVisible = true;
	}
	public void ConvertPunishmentPointsToPunishment(PunishmentType punishmentType)
	{
		//convert data code:
		var PunishPoint = _punishmentPointService.GetPointValue();

		PunishPoint -= punishmentType.PunishmentPoint;

		if (PunishPoint >= 0)
		{
			_punishmentPointService.AddValue(punishmentType.PunishmentPoint * -1);

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
		}
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

				//update also punishment what is desplayed on another page:

				var punishment = _punishmentService
					.GetAllPunishments()
					.FirstOrDefault(p => p.PunishmentTypeId == punishType.Id);

				if (punishment != null)
				{
					punishment.Description = punishType.Description;

					_punishmentService.UpdatePunishment(punishment);
				}
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
	private void UpdatePunishmentPointLabel(object sender, PunishmentPointValueChangedEventArgs e)
	{
		LabelPunishmentPoints.Text = _punishmentPointService.GetPointValue().ToString();
	}
}