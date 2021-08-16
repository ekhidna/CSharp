using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CSharp8Console.Interfaces;

namespace CSharp8Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            //Setup our DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IFooService, FooService>()
                .AddSingleton<IBarService, BarService>()               
                .AddSingleton<IAdding,Adding>()
                .AddSingleton<IOperations, Operations>()
                .AddLogging(loggingBuilder => loggingBuilder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug))
                .BuildServiceProvider();

            
            /**********  legacy way of adding logging ****************/
            //configure console logging
            //serviceProvider
            //    .GetService<ILoggerFactory>()
            //    .AddConsole(LogLevel.Debug);            
            

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");


            //var add = serviceProvider.GetService<IAdding>();
            //int x = add.CallAdd();
            //Console.WriteLine($"Adding 2+7 results in {x}");

            var add = serviceProvider.GetService<IOperations>();
            add.Sum();

            //do the actual work here
            var bar = serviceProvider.GetService<IBarService>();
            bar.DoSomeRealWork();

            logger.LogDebug("All done!");
          
        }        
    }
}
