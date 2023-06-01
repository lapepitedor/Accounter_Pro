using Accounter.Services;
using Accounter.ViewModels;

namespace Accounter.View;

public partial class Artikel_Seite : ContentPage
{
	public Artikel_Seite(ArtikelVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
    //protected override void OnAppearing()
    //{
    //   _ = vm.GetArtikelList();
    //}
}