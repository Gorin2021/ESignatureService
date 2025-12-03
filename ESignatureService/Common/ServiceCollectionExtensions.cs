using System.Reflection;
using ESignatureService.Interfaces.CQRS.Commands;

namespace ESignatureService.Common
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommandHandlers(this IServiceCollection services, Assembly assembly)
        {
            var commandHandlerTypes = assembly.GetTypes()
                .Where(t => t.Name.EndsWith("CommandHandler") && !t.IsInterface && !t.IsAbstract);

            foreach (var handlerType in commandHandlerTypes)
            {
                var interfaceType = handlerType.GetInterfaces().FirstOrDefault(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>));

                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, handlerType);
                }
            }
        }
    }
}
