using DiscordBotBones.Attributes;
using DiscordBotBones.Enums;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Configuration;
using System;

namespace DiscordBotBones.Services
{
    [Microservice(MicroserviceTypeEnum.InjectAndInitialize)]
    public class InteractivityService
    {
        public InteractivityService(IConfiguration config, DiscordClient discordClient)
        {
            discordClient.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(5),
                PollBehaviour = PollBehaviour.KeepEmojis,
                PaginationEmojis = new PaginationEmojis
                {
                    Stop = DiscordEmoji.FromName(discordClient, config["Interactivity:Pagination:StopEmoji"]),
                    Left = DiscordEmoji.FromName(discordClient, config["Interactivity:Pagination:LeftEmoji"]),
                    Right = DiscordEmoji.FromName(discordClient, config["Interactivity:Pagination:RightEmoji"]),
                    SkipLeft = DiscordEmoji.FromName(discordClient, config["Interactivity:Pagination:SkipLeftEmoji"]),
                    SkipRight = DiscordEmoji.FromName(discordClient, config["Interactivity:Pagination:SkipRightEmoji"]),
                },
                PaginationBehaviour = PaginationBehaviour.WrapAround,
                PaginationDeletion = PaginationDeletion.DeleteMessage
            });
        }
    }
}
