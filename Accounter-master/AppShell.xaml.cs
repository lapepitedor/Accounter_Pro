using Accounter.View;

namespace Accounter;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(Haupt_Seite), typeof(Haupt_Seite));
        Routing.RegisterRoute(nameof(Artikel_Seite), typeof(Artikel_Seite));
        Routing.RegisterRoute(nameof(NeuerArtikel_Seite), typeof(NeuerArtikel_Seite));
        Routing.RegisterRoute(nameof(Ausleihe_Seite), typeof(Ausleihe_Seite));
        Routing.RegisterRoute(nameof(Kunden_Seite), typeof(Kunden_Seite));
        Routing.RegisterRoute(nameof(Einkauf_Seite), typeof(Einkauf_Seite));
    }

    private void ShellContent_ChildAdded(object sender, ElementEventArgs e)
    {

    }
}
