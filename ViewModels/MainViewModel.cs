using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliJ.Lang.Annotations;
using Microsoft.Extensions.Logging;
using WOWStore.Data;
using WOWStore.Pages;
using WOWStore.Services;
using WOWStore.Services.Models;

namespace WOWStore.ViewModels;


public partial class MainViewModel : ObservableObject
{
    private ApiService _apiService;

    [ObservableProperty]
    private ObservableCollection<Product> products;
    [ObservableProperty]
    private bool isLoading = false;
    [ObservableProperty]
    private bool listIsVisible = false;
    [ObservableProperty]
    private string gridImageSource = "layout_grid_disabled.svg";
    [ObservableProperty]
    private string listImageSource = "layout_list_enabled.svg";


    [ObservableProperty]
    private bool listSelectionIsVisible = false;
    [ObservableProperty]
    private bool gridSelectionIsVisible = true;
    [ObservableProperty]
    private int columnGridSpan = 1;
    [ObservableProperty]
    private bool isBusy;
    [ObservableProperty]
    private string busyText;

    public MainViewModel(ApiService apiService)
    {
        _apiService = apiService;
        Products = new ObservableCollection<Product>();
    }

    public async Task<bool> GetItems()
    {
        Logger.LogInfo("Call from api request");
        IsLoading = true;
        ListIsVisible = false;
        var results = await _apiService.GetProductsAsync();

        if (results != null)
        {
            try
            {
                _ = ProductsService.RemoveAllProducts();
                foreach(Product product in results) {
                    await ProductsService.InsertProduct(product);
                }
                RefreshListO();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
            //Products.Clear();
            //foreach (var item in results)
            //{
            //    Products.Add(item);
            //}
            //IsLoading = false;
            //ListIsVisible = true;
            return true;
        }
        else
        {
            IsLoading = false;
            ListIsVisible = true;
            return false;
        }
    }

    [RelayCommand]
    public void RefreshList()
    {
        _ = GetItems();
    }

    [RelayCommand]
    public void GridSelection()
    {
        ColumnGridSpan = 2;
        GridSelectionIsVisible = false;
        ListSelectionIsVisible = true;
        GridImageSource = "layout_grid_enabled.svg";
        ListImageSource = "layout_list_disabled.svg";

    }

    [RelayCommand]
    public void ListSelection()
    {
        ColumnGridSpan = 1;
        GridSelectionIsVisible = true;
        ListSelectionIsVisible = false;
        ListImageSource = "layout_list_enabled.svg";
        GridImageSource = "layout_grid_disabled.svg";

    }

    [RelayCommand]
    public void OpenFavourites()
    {
        Shell.Current.GoToAsync(nameof(FavouritesPage));

    }

    async void RefreshListO()
    {
        Products.Clear();
        var list = await ProductsService.GetAllProducts();

        Logger.LogInfo("List from db: " + list.Count);
        if (list.Count == 0) {
            _ = GetItems();
            return;
        }

        foreach (var item in list)
        {
            if (item.isFavorite)
            {
                item.FavoriteImage = "heart_selected.svg";
            }
            else
            {
                item.FavoriteImage = "heart.svg";
            }

            Products.Add(item);
        }
        IsLoading = false;
        ListIsVisible = true;
    }


    public void GetAllProducts()
    {
        RefreshListO();
        Logger.LogInfo("laod products");
      
    }


}

