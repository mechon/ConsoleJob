await Host.CreateDefaultBuilder(args)
          .ConfigureAppConfiguration(AppConfiguration.Set)
          .UseSerilog(AppLogger.Set)
          .ConfigureServices(AppServices.Set)
          .UseDefaultServiceProvider(opts => opts.ValidateScopes = false)
          .RunConsoleAsync();
