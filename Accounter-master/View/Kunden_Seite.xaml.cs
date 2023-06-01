using Accounter.ViewModels;

namespace Accounter.View;

public partial class Kunden_Seite : ContentPage
{
	public Kunden_Seite(KundenVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}