using WOWStore.Pages;

namespace WOWStore;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(ProductDetailsPage), typeof(ProductDetailsPage));
        Routing.RegisterRoute(nameof(FavouritesPage), typeof(FavouritesPage));
    }
}

