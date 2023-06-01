using Accounter.ViewModels;

namespace Accounter.View;

public partial class Einkauf_Seite : ContentPage
{
	public Einkauf_Seite(EinkaufVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}