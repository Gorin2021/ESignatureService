using System.Reflection;
using ESignatureService.Common.CQRS.Command;
using ESignatureService.Common.CQRS.Query;
using ESignatureService.CQRS.Queries;
using ESignatureService.Interfaces;
using ESignatureService.Interfaces.CQRS.Commands;
using ESignatureService.Interfaces.CQRS.Queries;
using ESignatureService.Interfaces.Services;
using ESignatureService.Models;
using ESignatureService.Services;
using Microsoft.EntityFrameworkCore;

namespace ESignatureService.Common;

public class DependencyInjectionConfiguration
{
    public static void Configure(IServiceCollection sc, IConfiguration configuration)
    {
        sc.AddHttpClient();
        sc.AddDbContext<SNSIDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        sc.AddScoped<ICommandDispatcher, CommandDispatcher>();
        sc.AddScoped<IQueryHandler<GetCertificatesBySnilsQuery, IEnumerable<PublicCertificateViewModel>>,
            GetCertificatesBySnilsQueryHandler>();
        sc.AddScoped<IQueryHandler<GetRTMISCertificateIdBySNILSOrIdQuery, long?>, GetRTMISCertificateIdBySNILSOrIdQueryHandler>();
        sc.AddScoped<IQueryDispatcher, QueryDispatcher>();
        sc.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        sc.AddCommandHandlers(Assembly.GetExecutingAssembly());
        sc.AddScoped<IECPService, EcpService>();
        sc.AddScoped<IHttpClientService, HttpClientService>();
    }
}