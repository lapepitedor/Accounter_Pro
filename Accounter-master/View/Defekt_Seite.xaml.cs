using Accounter.ViewModels;

namespace Accounter.View;

public partial class Defekt_Seite : ContentPage
{
	public Defekt_Seite(DefektVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}