using DiscordBotBones.Attributes;
using DiscordBotBones.Enums;
using DSharpPlus;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DiscordBotBones.Services
{
    [Microservice(MicroserviceTypeEnum.InjectAndInitialize)]
    public class EventService
    {
        private readonly IConfiguration _configuration;
        public readonly EventId BotEventId = new(42, "DiscordBotBones");
        public EventService(IConfiguration configuration, DiscordClient client)
        {
            _configuration = configuration;
            client.GuildMemberAdded += Client_GuildMemberAdded;
            client.MessageReactionAdded += Client_MessageReactionAdded;
            client.ClientErrored += Client_ClientError;
            client.Ready += OnClientReady;
            client.MessageCreated += Client_MessageCreated;
            client.GuildMemberUpdated += Client_GuildMemberUpdated;
        }

        private async Task Client_GuildMemberUpdated(DiscordClient sender, GuildMemberUpdateEventArgs e)
        {
            await Task.CompletedTask;
        }

        private async Task Client_MessageCreated(DiscordClient sender, MessageCreateEventArgs e)
        {
            await Task.CompletedTask;
        }

        private async Task Client_MessageReactionAdded(DiscordClient sender, MessageReactionAddEventArgs e)
        {
            await Task.CompletedTask;
        }

        private async Task Client_GuildMemberAdded(DiscordClient sender, GuildMemberAddEventArgs e)
        {
            await Task.CompletedTask;
        }

        private Task Client_ClientError(DiscordClient sender, ClientErrorEventArgs e)
        {
            sender.Logger.LogError(BotEventId, e.Exception, $"Exception occurred: {e.Exception.Message}");
            return Task.CompletedTask;
        }

        private async Task OnClientReady(DiscordClient sender, ReadyEventArgs e)
        {
            sender.Logger.LogInformation(BotEventId, "Ready!");
            await Task.CompletedTask;
        }
    }
}
