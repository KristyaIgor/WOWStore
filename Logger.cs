using System;
using Microsoft.Extensions.Logging;

namespace WOWStore;

public static class Logger
{
    private static ILogger<MainPage> logger;

    static void Init()
	{
        if (logger != null)
            return;
        var serviceProvider = new ServiceCollection()
          .AddLogging(builder =>
          {
              //builder.AddConsole(); // Logs to the console
              builder.AddDebug();   // Logs to the debug output
          })
          .BuildServiceProvider();

        // Get a logger instance
        logger = serviceProvider.GetRequiredService<ILogger<MainPage>>();
    }


    public static void LogInfo(string message)
    {
        Init();
        logger.LogInformation(message);
    }

    public static void LogError(Exception ex)
    {
        Init();
        logger.LogError(ex, null);
    }
}

