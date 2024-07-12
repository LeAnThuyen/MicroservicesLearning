using Infrastructure.Extensions;
using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ordering.API.Application.IntegrationEvents.EventsHandler;
using Ordering.Infrastructure.Configurations;
using Shared.Configurations;

namespace Ordrering.API.Extentions;

public static class ServiceExtentions
{
    internal static IServiceCollection AddConfigurationServiceSettings(this IServiceCollection services,
        IConfiguration configuration)
    {
        var emailSettings = configuration.GetSection(nameof(SmtpEmailSetting)).Get<SmtpEmailSetting>();
        services.AddSingleton(emailSettings);
        
        var eventBusSettings = configuration.GetSection(nameof(EventBusSettings)).Get<EventBusSettings>();
        services.AddSingleton(eventBusSettings);
        return services;
    }
    public static void ConfigMasstransit(this IServiceCollection services)
    {
        var settings = services.GetOptions<EventBusSettings>("EventBusSettings");
        if (string.IsNullOrEmpty(settings.HostAddress))
        {
            throw new ArgumentNullException("EventBusSettings is not configured.");
        }

        var mqConnection = new Uri(settings.HostAddress);
        services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
        // "BasketCheckoutEventQueue"=>"basket-checkout-event-queue";
        services.AddMassTransit(config =>
        {
            config.AddConsumersFromNamespaceContaining<BasketCheckoutEventHandler>();
            config.UsingRabbitMq((ctx, cfg) =>
            {
                // cfg.Host(mqConnection);
                // cfg.ReceiveEndpoint("basket-checkout-queue", c =>
                // {
                //     c.ConfigureConsumer<BasketCheckoutConsumer>();
                // });
                cfg.ConfigureEndpoints(ctx);
            });
            // Publish submit order message
            //config.AddRequestClient<IBasketCheckoutEvent>();
        });
            
    }
}