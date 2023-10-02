namespace WOWStore.Services.Models;

public class ProductResult
{
    public int count { get; set; }
    public int total_pages { get; set; }
    public int per_page { get; set; }
    public int current_page { get; set; }
    public List<Product> results { get; set; }
}