using DSharpPlus.CommandsNext.Attributes;

namespace DiscordBotBones.Attributes
{
    public abstract class DescriptiveCheckBaseAttribute : CheckBaseAttribute
    {
        public string FailureResponse { get; set; }
    }
}
