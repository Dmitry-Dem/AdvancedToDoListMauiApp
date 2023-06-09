using AdvancedToDoListMauiApp.Data;
using Microsoft.Extensions.Logging;

namespace AdvancedToDoListMauiApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		/*
		//var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		//return Path.Combine(basePath, DatabaseFilename);

		string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data/DataBase.db");

		builder.Services.AddSingleton(s =>
		ActivatorUtilities.CreateInstance<ApplicationDb>(s, dbPath));*/



#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
