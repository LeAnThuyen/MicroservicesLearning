using Ordering.Infrastructure.Configurations;

namespace Ordrering.API.Extentions;

public static class ServiceExtentions
{
    internal static IServiceCollection AddConfigurationServiceSettings(this IServiceCollection services,
        IConfiguration configuration)
    {
        var emailSettings = configuration.GetSection(nameof(SmtpEmailSetting)).Get<SmtpEmailSetting>();
        services.AddSingleton(emailSettings);
        return services;
    }
    
}