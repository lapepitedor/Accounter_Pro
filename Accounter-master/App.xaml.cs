using Accounter.Services;
using Accounter.View;
using Accounter.ViewModels;

namespace Accounter;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new Anmelde_Seite(new Anmelde_SeiteViewModel(new BenutzerService()));
		
	}
}
