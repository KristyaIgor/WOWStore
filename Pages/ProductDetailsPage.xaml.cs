using WOWStore.ViewModels;

namespace WOWStore.Pages;

public partial class ProductDetailsPage : ContentPage
{
	public ProductDetailsPage(ProductDetailsViewModel viewModel)
	{

        InitializeComponent();
        BindingContext = viewModel;
    }
}
