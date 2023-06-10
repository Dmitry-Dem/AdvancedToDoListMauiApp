using AdvancedToDoListMauiApp.Data;
using AdvancedToDoListMauiApp.Views;

namespace AdvancedToDoListMauiApp;

public partial class App : Application
{
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
