using Microsoft.Extensions.Logging;
using WOWStore.Services.Models;

namespace WOWStore.Services;

public class ApiService
{
    private const string baseUrl = "http://mobile-shop-api.hiring.devebs.net/";

    public async Task<List<Product>> GetProductsAsync()
    {
        using var client = new HttpClient();
        // Construct the full API URL
        string apiUrl = $"{baseUrl}products";
        var serviceProvider = new ServiceCollection()
           .AddLogging(builder =>
           {
               //builder.AddConsole(); // Logs to the console
               builder.AddDebug();   // Logs to the debug output
           })
           .BuildServiceProvider();

        // Get a logger instance
        ILogger<MainPage> logger = serviceProvider.GetRequiredService<ILogger<MainPage>>();

        try
        {
            // Send a GET request to the API
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                // Parse the response content to your model class
                string content = await response.Content.ReadAsStringAsync();
                logger.LogInformation("Respons: " + content);
                ProductResult myData = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductResult>(content);
                return myData.results;
            }
            else
            {
                // Handle error responses
                // You can throw an exception or return an error model
                logger.LogInformation("Response not succes: " + response.StatusCode);
                return null;
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions
            logger.LogError( "Error to api: " + ex.Message);
            return null;
        }
    }

}