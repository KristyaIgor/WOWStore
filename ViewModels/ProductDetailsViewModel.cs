
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WOWStore.Pages;
using WOWStore.Services.Models;

namespace WOWStore.ViewModels;

[QueryProperty(nameof(Name),nameof(Name))]
[QueryProperty(nameof(ProductItem), nameof(ProductItem))]

public partial class ProductDetailsViewModel : ObservableObject
{

    [ObservableProperty]
    string name;

    [ObservableProperty]
    Product productItem;

   
    public ProductDetailsViewModel()
	{
       
    }


    [RelayCommand]
    void GoBack() => Shell.Current.GoToAsync("..");
    

    [RelayCommand]
    void GoToOther()
    {
        Shell.Current.GoToAsync($"../{nameof(FavouritesPage)}");
    }

}
