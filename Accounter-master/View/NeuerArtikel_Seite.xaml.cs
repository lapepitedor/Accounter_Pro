using Accounter.ViewModels;

namespace Accounter.View;

public partial class NeuerArtikel_Seite : ContentPage
{
	public NeuerArtikel_Seite( ArtikelVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}