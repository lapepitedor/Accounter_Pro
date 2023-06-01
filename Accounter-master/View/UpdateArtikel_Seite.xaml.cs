using Accounter.Models;
using Accounter.ViewModels;

namespace Accounter.View;

public partial class UpdateArtikel_Seite : ContentPage
{
	public UpdateArtikel_Seite(ArtikelVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}