using DiscordBotBones.Attributes;
using DiscordBotBones.Constants;
using DiscordBotBones.Enums;
using DiscordBotBones.ExtensionMethods;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordBotBones.Services
{
    [Microservice(MicroserviceTypeEnum.InjectAndInitialize)]
    public class CommandService
    {
        public CommandService(IConfiguration config, DiscordClient discordClient, IServiceProvider services)
        {
            var commands = discordClient.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = new string[]
                {
                    config["Discord:commandPrefix"]
                },
                EnableDms = true,
                CaseSensitive = false,
                EnableMentionPrefix = true,
                DmHelp = true,
                Services = services
            });
            commands.CommandErrored += Commands_CommandErrored;
            commands.RegisterCommands(Assembly.GetEntryAssembly());
        }

        private async Task Commands_CommandErrored(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            if (e.Exception is ChecksFailedException exception)
            {
                var embed = new DiscordEmbedBuilder
                {
                    Title = ResourceStrings.AccessDeniedTitle,
                    Description = exception.FailedChecks.ToCleanResponse(),
                    Color = DiscordColor.Red
                };

                await e.Context.RespondAsync(embed: embed);
            }
            else
            {
                e.Context.Client.Logger.LogError(string.Format(ErrorStrings.CommandErrored, e.Context.User.Username, e.Command?.QualifiedName ?? ResourceStrings.UnknownCommand, e.Exception.GetType(), e.Exception.Message ?? ResourceStrings.NoMessage));
            }
        }
    }
}
