using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using WOWStore.Data;
using WOWStore.Pages;
using WOWStore.Services;
using WOWStore.ViewModels;

namespace WOWStore;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("open_sans_regular.ttf", "OpenSansRegular");
				fonts.AddFont("open_sans_semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<ProductDetailsViewModel>();
		builder.Services.AddTransient<ProductDetailsPage>();
        builder.Services.AddSingleton<ApiService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

