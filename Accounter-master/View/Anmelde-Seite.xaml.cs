using Accounter.ViewModels;


namespace Accounter.View;
public partial class Anmelde_Seite : ContentPage
{
	public Anmelde_Seite(Anmelde_SeiteViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
	}
}