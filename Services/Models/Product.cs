
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SQLite;

namespace WOWStore.Services.Models;

public partial class Product : ObservableObject
{
    //[ObservableProperty]
    //public Category category;
    [ObservableProperty]
    public string name;
    [ObservableProperty]
    public string details;
    [ObservableProperty]
    public string size;
    [ObservableProperty]
    public string colour;
    [ObservableProperty]
    public double price;
    [ObservableProperty]
    private string main_image;
    public int id { get; set; }

    [PrimaryKey, AutoIncrement]
    public int identifyer { get; set; }
    public bool isFavorite { get; set; }
    public bool isAddedToCart { get; set; }

    [ObservableProperty]
    public string favoriteImage = "heart.svg";

    [ObservableProperty]
    private string cartImage = "cart.svg";

    [ObservableProperty]
    private string cartImageColor = "#FFFFFF";

    public Product Clone() => MemberwiseClone() as Product;


    [RelayCommand]
    public async Task SetFavorite()
    { 
        isFavorite = !isFavorite;

        try
        {
            await ProductsService.SetFavoriteProduct(this.id);
        }

        catch (Exception ex)
        {
            this.Details = ex.Message;
            Logger.LogInfo("Error write: " + ex.Message);
            Logger.LogError(ex);
        }

        if (isFavorite)
        {
            FavoriteImage = "heart_selected.svg";
        }
        else
        {
            FavoriteImage = "heart.svg";
        }
    }

    [RelayCommand]
    public void AddToCart()
    {
        isAddedToCart = !isAddedToCart;
        if (isAddedToCart)
        {
            CartImage = "cart_added.svg";
            CartImageColor = "#07195C";
        }
        else
        {
            CartImage = "cart.svg";
            CartImageColor = "#FFFFFF";
        }
    }

}