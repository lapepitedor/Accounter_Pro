using Accounter.ViewModels;

namespace Accounter.View;

public partial class Ausleihe_Seite : ContentPage
{
	public Ausleihe_Seite(AusleiheVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}