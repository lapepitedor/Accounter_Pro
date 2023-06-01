using Accounter.ViewModels;

namespace Accounter.View;

public partial class NeuerDefekt_Seite : ContentPage
{
	public NeuerDefekt_Seite(DefektVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}