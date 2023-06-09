using AdvancedToDoListMauiApp.Data;

namespace AdvancedToDoListMauiApp;

public partial class App : Application
{
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
