
using SQLite;
using WOWStore.Services.Models;

namespace WOWStore.Services;

public static class ProductsService
{
    static SQLiteAsyncConnection db;
    static async Task Init() {
        if (db != null)
            return;
        // Get an absolute path to the database file
        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData3.db3");

        db = new SQLiteAsyncConnection(databasePath);
        await db.CreateTableAsync<Product>();
        await db.CreateTableAsync<Category>();
    }

    public static async Task InsertProduct(Product product)
    {
        await Init();
        await db.InsertAsync(product);
    }

    public static async Task InsertAllProducts(List<Product> products)
    {
        await Init();
        
        await db.InsertAllAsync(products);
    }

    public static async Task<List<Product>> GetAllProducts()
    {
        await Init();
        return await db.Table<Product>().ToListAsync();
    }

    public static async Task<Product> GetProduct(int id)
    {
        await Init();
        var query = db.Table<Product>().Where(s => s.id == id);

        return await query.FirstOrDefaultAsync();
    }


    public static async Task UpdateProduct(Product item)
    {
        await Init();
        var test = await db.UpdateAsync(item);
        Logger.LogInfo("Result the update the product:" + test.ToString());
    }

    public static async Task SetFavoriteProduct(int id)
    {
        await Init();
        var query = db.Table<Product>().Where(s => s.id == id);
        var item = await query.FirstAsync();
        Logger.LogInfo("Finded item" + item.Name + " and is favorite: " + item.isFavorite);
        item.isFavorite = !item.isFavorite;
        await UpdateProduct(item);
    }

    public static async Task RemoveProduct (int id)
    {
        await Init();
        await db.DeleteAsync<Product>(id);
    }

    public static async Task RemoveAllProducts()
    {
        await Init();
        await db.DeleteAllAsync<Product>();
    }
}

