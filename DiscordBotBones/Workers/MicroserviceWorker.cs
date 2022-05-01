using DiscordBotBones.Attributes;
using DiscordBotBones.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DiscordBotBones.Workers
{
    public static class MicroserviceWorker
    {
        public static IServiceCollection InjectMicroservices(this IServiceCollection services, Assembly assembly)
        {
            Dictionary<Type, MicroserviceAttribute> typesToLoad = new();
            var exportedTypes = assembly.ExportedTypes;
            foreach (var exportedType in exportedTypes)
            {
                var exportedTypeInfo = exportedType.GetTypeInfo();
                if (exportedTypeInfo.GetCustomAttributes().Any(x => x.GetType() == typeof(MicroserviceAttribute)))
                {
                    typesToLoad.Add(exportedType, exportedType.GetCustomAttribute<MicroserviceAttribute>());
                }
            }

            foreach (var type in typesToLoad.OrderBy(type => type.Value.Order))
            {

                if (type.Value.Type == MicroserviceTypeEnum.Inject || type.Value.Type == MicroserviceTypeEnum.InjectAndInitialize)
                {
                    services.AddSingleton(type.Key);
                }
            }

            return services;
        }

        public static void InitializeMicroservices(this IServiceProvider services, Assembly assembly)
        {
            Dictionary<Type, MicroserviceAttribute> typesToLoad = new();
            var exportedTypes = assembly.ExportedTypes;
            foreach (var exportedType in exportedTypes)
            {
                var exportedTypeInfo = exportedType.GetTypeInfo();
                if (exportedTypeInfo.GetCustomAttributes().Any(x => x.GetType() == typeof(MicroserviceAttribute)))
                {
                    typesToLoad.Add(exportedType, exportedType.GetCustomAttribute<MicroserviceAttribute>());
                }
            }

            foreach (var type in typesToLoad.OrderBy(type => type.Value.Order))
            {

                if (type.Value.Type == MicroserviceTypeEnum.InjectAndInitialize)
                {
                    services.GetRequiredService(type.Key);
                }
            }
        }
    }
}
