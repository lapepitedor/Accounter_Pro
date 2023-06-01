namespace Accounter.View;
using Accounter.ViewModels;

public partial class NeuerKunde_Seite : ContentPage
{
	public NeuerKunde_Seite(KundenVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}