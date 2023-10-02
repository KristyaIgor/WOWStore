using CommunityToolkit.Maui.Core;
using WOWStore.Services.Models;
using WOWStore.ViewModels;
using CommunityToolkit.Maui.Alerts;
using WOWStore.Pages;

namespace WOWStore;

public partial class MainPage : ContentPage
{

    public MainPage(MainViewModel viewModel)
	{
        InitializeComponent();

        BindingContext = viewModel;
       
        viewModel.GetAllProducts();
    }

    void OnItemSelected(object sender, SelectionChangedEventArgs args)
    {
        Product selectedProduct = args.CurrentSelection.FirstOrDefault() as Product;

        Toast.Make("Tapped item: " + selectedProduct.Name , ToastDuration.Short).Show();

        Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}?Name={selectedProduct.Name}", new Dictionary<string, object> { ["ProductItem"] = selectedProduct});
    }
}


