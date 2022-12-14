await Host.CreateDefaultBuilder(args)
          .SetAppConfig()
          .AddSerilog()
          .AddCustomServices()
          .UseDefaultServiceProvider(opts => opts.ValidateScopes = false)
          .RunConsoleAsync();
