using System.Collections.ObjectModel;
using System.Windows.Input;
using AdvancedToDoListMauiApp.Args;
using AdvancedToDoListMauiApp.Services.Interfaces;
using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Services;

namespace AdvancedToDoListMauiApp.Views;

public partial class PointsConverterPage : ContentPage
{
	public ObservableCollection<PunishmentType> PunishmentTypes { get; set; } = new ObservableCollection<PunishmentType>();

	private readonly IPunishmentService _punishmentService = new PunishmentService();
	private readonly IPunishmentTypeService _punishmentTypeService = new PunishmentTypeService();
	private readonly IPunishmentPointService _punishmentPointService = new PunishmentPointService();
	public ICommand DeletePunishmentTypeCommand { get; }
	public ICommand UpdatePunishmentTypeCommand { get; }
	public ICommand ConvertPunishmentTypeCommand { get; }
	public PointsConverterPage()
	{
		InitializeComponent();

		DeletePunishmentTypeCommand = new Command<PunishmentType>(DeletePunishmentType);
		ConvertPunishmentTypeCommand = new Command<PunishmentType>(ConvertPunishmentPointsToPunishment);
		UpdatePunishmentTypeCommand = new Command<PunishmentType>(UpdatePunishmentType);

		PunishmentPointService.PunishmentPointChanged += UpdatePunishmentPointLabel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var value = await _punishmentPointService.GetPointValueAsync();

        UpdatePunishmentPointLabel(this, new PunishmentPointValueChangedEventArgs("", value, 0));

		await InitialCollectionCreation();
    }
    private void ClosePunishmentTypePanel()
	{
		PanelPunishmenType.IsVisible = false;
		PanelLabelPunishmentTypeId.Text = string.Empty;
		EntryPunishmentTypeName.Text = string.Empty;
		EntryPunishmentTypeValue.Text = string.Empty;
		EntryPunishmentTypePoint.Text = string.Empty;
	}
	private async Task UpdatePunishmentTypesCollectionAsync()
	{
		var punishTypesList = await _punishmentTypeService.GetAllPunishmentTypesAsync();

		PunishmentTypes.Clear();

		foreach (var punType in punishTypesList)
			PunishmentTypes.Add(punType);
	}
	private void ButtonBack_Clicked(object sender, EventArgs e)
	{
		Navigation.PopAsync();
	}
	public async void DeletePunishmentType(PunishmentType punishmentType)
	{
		var result = await _punishmentTypeService.DeletePunishmentTypeAsync(punishmentType);

		if (result > 0)
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
	private async Task InitialCollectionCreation()
	{
		PunishmentTypes.Clear();
		BindingContext = this;

        var list = await _punishmentTypeService.GetAllPunishmentTypesAsync().ConfigureAwait(false);

        foreach (var item in list)
        {
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
	public async void ConvertPunishmentPointsToPunishment(PunishmentType punishmentType)
	{
		var PunishPoint = await _punishmentPointService.GetPointValueAsync();

		PunishPoint -= punishmentType.PunishmentPoint;

		if (PunishPoint < 0)
			return;
		
		await _punishmentPointService.AddValueAsync(punishmentType.PunishmentPoint * -1);

		var dbPunishment = await _punishmentService.GetPunishmentByTypeIdAsync(punishmentType.Id);

		if (dbPunishment != null)
		{
			dbPunishment.Value += punishmentType.Value;

			await _punishmentService.UpdatePunishmentAsync(dbPunishment);
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

			await _punishmentService.AddPunishmentAsync(newPunishment);
		}
	}
	private void TapBorderClosePanelButton_Tapped(object sender, TappedEventArgs e)
	{
		ClosePunishmentTypePanel();
	}
	private async void TapBorderSavePanelDataButton_Tapped(object sender, TappedEventArgs e)
	{
		if (!IsPanelPunishmentTypeDataValid(EntryPunishmentTypeName.Text, EntryPunishmentTypeValue.Text, EntryPunishmentTypePoint.Text))
			return;
		
		int result = 0;

		bool punishmentTypeExist = !string.IsNullOrEmpty(PanelLabelPunishmentTypeId.Text);

		if (punishmentTypeExist)
		{
			int punishTypeId = int.Parse(PanelLabelPunishmentTypeId.Text);

			var punishType = await _punishmentTypeService.GetPunishmentTypeByIdAsync(punishTypeId);

			if (punishType == null) return;

			punishType.Description = EntryPunishmentTypeName.Text;
			punishType.Value = int.Parse(EntryPunishmentTypeValue.Text);
			punishType.PunishmentPoint = int.Parse(EntryPunishmentTypePoint.Text);

			await _punishmentTypeService.UpdatePunishmentTypeAsync(punishType);

			var punishment = await _punishmentService.GetPunishmentByTypeIdAsync(punishTypeId);

			if (punishment != null)
			{
				punishment.Description = punishType.Description;

				result = await _punishmentService.UpdatePunishmentAsync(punishment);
			}
		}
		else 
		{
			var newPunishType = new PunishmentType()
			{
				Description = EntryPunishmentTypeName.Text,
				Value = int.Parse(EntryPunishmentTypeValue.Text),
				PunishmentPoint = int.Parse(EntryPunishmentTypePoint.Text)
			};

			result = await _punishmentTypeService.AddPunishmentTypeAsync(newPunishType);
		}

		ClosePunishmentTypePanel();

		if (result > 0)
			await UpdatePunishmentTypesCollectionAsync();
	}
	private void UpdatePunishmentPointLabel(object? sender, PunishmentPointValueChangedEventArgs e)
	{
		LabelPunishmentPoints.Text = e.Value.ToString();
	}
	private static bool IsPanelPunishmentTypeDataValid(string description,  string value, string pp)
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
}