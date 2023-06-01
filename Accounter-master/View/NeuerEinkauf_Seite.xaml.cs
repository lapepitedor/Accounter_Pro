using Accounter.ViewModels;

namespace Accounter.View;

public partial class NeuerEinkauf_Seite : ContentPage
{
	public NeuerEinkauf_Seite(EinkaufVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}