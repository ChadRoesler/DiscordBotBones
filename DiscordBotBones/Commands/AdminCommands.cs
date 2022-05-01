using DiscordBotBones.Constants;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace DiscordBotBones.Commands
{
    [Group(CommandGroupStrings.AdminCommand)]
    [Description(CommandGroupStrings.AdminCommandDescription)]
    [RequireOwner()]
    public class AdminCommands : BaseCommandModule
    {
        private readonly IConfiguration _configuration;
        public AdminCommands(IConfiguration configuration) => _configuration = configuration;

        [Command(CommandStrings.AdminKill)]
        [Description(CommandStrings.AdminKillDescription)]
        [RequireOwner()]
        public async Task Kill(CommandContext ctx)
        {
            await ctx.RespondAsync(MessageStrings.GoodByeMessage);
            Environment.Exit(0);
        }

        [Command(CommandStrings.AdminUptime)]
        [Description(CommandStrings.AdminUptimeDescription)]
        public async Task GetUptime(CommandContext ctx)
        {
            TimeSpan uptime = DateTime.Now.Subtract(Program.Startup);
            await ctx.RespondAsync(string.Format(ResponseStrings.UptimeMessage, uptime.Days, uptime.Hours, uptime.Minutes, uptime.Seconds));
        }

        [Command(CommandStrings.AdminAnnounce)]
        [Description(CommandStrings.AdminAnnounceDescription)]
        [RequireRoles(RoleCheckMode.All, "Mod")]
        public async Task Announce(CommandContext ctx, [Description(ParameterStrings.AdminAnnounceChannel)] DiscordChannel channel, [Description(ParameterStrings.AdminAnnounceMessage)][RemainingText] string message)
        {
            await channel.SendMessageAsync(message);
        }
    }
}
