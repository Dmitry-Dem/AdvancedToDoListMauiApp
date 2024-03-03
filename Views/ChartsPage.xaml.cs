using AdvancedToDoListMauiApp.Models.DTOs;
using AdvancedToDoListMauiApp.Services;
using AdvancedToDoListMauiApp.Services.Interfaces;
using System.Collections.ObjectModel;

namespace AdvancedToDoListMauiApp.Views;
public partial class ChartsPage : ContentPage
{
    public ObservableCollection<PeriodItemDto> ActivityPeriods { get; set; } = new();
    public ObservableCollection<PunishmentLogDto> PunishmentChanges { get; set; } = new();

    private readonly IPunishmentChangesService _punishmentChanges = new PunishmentChangesService();
    public ChartsPage()
	{
		InitializeComponent();

        InitializeData();
    }
    private async void InitializeData()
    {
        BindingContext = this;

        var periods = new List<PeriodItemDto>
        {
            new ("1 Day", 1),
            new ("1 Week", 7),
            new ("1 Month", 30),
            new ("6 Months", 182),
            new ("1 Year", 365)
        };

        foreach (var period in periods)
            ActivityPeriods.Add(period);

        await UpdatePunishmentChangesCollection(TimeSpan.FromDays(1));
    }
    private async Task UpdatePunishmentChangesCollection(TimeSpan timeSpan)
    {
        if (PunishmentChanges.Count > 0)
            PunishmentChanges.Clear();

        var logs = await _punishmentChanges.GetPunishmentLogsAsync(timeSpan);

        foreach (var log in logs)
            PunishmentChanges.Add(log);

        LableNoLogMessage.IsVisible = PunishmentChanges.Count <= 0;
    }
    private async void SelectPeriod_Tapped(object sender, TappedEventArgs e)
    {
        var periodBorder = sender as Border;

        if (periodBorder == null)
            return;

        int periodInDays = int.Parse(periodBorder.ClassId);

        await UpdatePunishmentChangesCollection(TimeSpan.FromDays(periodInDays));
    }
}