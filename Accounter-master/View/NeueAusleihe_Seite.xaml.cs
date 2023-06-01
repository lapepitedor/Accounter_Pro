using Accounter.ViewModels;

namespace Accounter.View;

public partial class NeueAusleihe_Seite : ContentPage
{
	public NeueAusleihe_Seite(AusleiheVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}