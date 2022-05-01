using DiscordBotBones.Enums;
using System;

namespace DiscordBotBones.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MicroserviceAttribute : Attribute
    {
        public MicroserviceAttribute(MicroserviceTypeEnum microserviceType = MicroserviceTypeEnum.Manual, int order = 99)
        {
            Type = microserviceType;
            Order = order;
        }
        public MicroserviceTypeEnum Type { get; }
        public int Order { get; set; }
    }
}
