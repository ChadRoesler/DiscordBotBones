using DiscordBotBones.Attributes;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiscordBotBones.ExtensionMethods
{
    internal static class CheckBaseAttributeExtensions
    {
        private const string ReasonPrefix = " • ";

        public static string ToCleanResponse(this IEnumerable<CheckBaseAttribute> failedChecks)
        {
            var reasons = failedChecks.Select(x => x.ToCleanReason());

            return ReasonPrefix + string.Join(Environment.NewLine + ReasonPrefix, reasons);
        }

        private static string ToCleanReason(this CheckBaseAttribute check)
        {
            if (check is DescriptiveCheckBaseAttribute descriptiveCheck)
            {
                return descriptiveCheck.FailureResponse;
            }

            return check.ToString();
        }
    }
}
