using DiscordBotBones.Workers;
using DSharpPlus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBotBones.Models
{
    class Bot : IHostedService
    {
        private readonly DiscordClient _discordClient;
        public IConfiguration Configuration { get; }
        public ILoggerFactory Logger { get; }

        public Bot(IConfiguration configuration)
        {
            Configuration = configuration;
            Logger = new LoggerFactory().AddLog4Net();

            var config = new DiscordConfiguration
            {
                Token = Configuration["Discord:Token"],
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LoggerFactory = Logger,
                MinimumLogLevel = LogLevel.Debug,
                Intents = DiscordIntents.All
            };
            _discordClient = new DiscordClient(config);
            var services = BuildServiceProvider();
            services.InitializeMicroservices(Assembly.GetEntryAssembly());
        }

        private IServiceProvider BuildServiceProvider()
            => new ServiceCollection()
                .AddSingleton(Configuration)
                .AddSingleton(Logger)
                .AddSingleton(_discordClient)
                .InjectMicroservices(Assembly.GetEntryAssembly())
                .BuildServiceProvider();
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _discordClient.ConnectAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _discordClient.DisconnectAsync();
        }
    }
}
