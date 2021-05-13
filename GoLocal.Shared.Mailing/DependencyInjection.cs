using GoLocal.Shared.Mailing.Commons.Configurations;
using GoLocal.Shared.Mailing.Implementations;
using GoLocal.Shared.Mailing.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Shared.Mailing
{
    public static class DependencyInjection
    {
        public static void SetupMailing(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<EmailConfiguration>(m => configuration.GetSection(nameof(EmailConfiguration)).Bind(m));
            services.Configure<EmailConfiguration>(configuration.GetSection(nameof(EmailConfiguration)));
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}