using System.Net;
using Elasticsearch.Net;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.OpenSearch;

namespace WebApplication3.Extentions;

public static class LoggerExtentions
{
     public static void AddLogger(this WebApplicationBuilder builder)
        {
            
            builder.Logging.ClearProviders();
    
    
            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));
    
            ServicePointManager.ServerCertificateValidationCallback = (o, certificate, chain, errors) => true;
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidations.AllowAll;
    
    
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.OpenSearch(new OpenSearchSinkOptions(new Uri("https://localhost:9200"))
                {
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    MinimumLogEventLevel = LogEventLevel.Verbose,
                    TypeName = "_doc",
                    InlineFields = false,
                    ModifyConnectionSettings = x =>
                        x.BasicAuthentication("admin", "bars123@superMyPassword")
                            .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
                            .ServerCertificateValidationCallback((o, certificate, chain, errors) => true),
                    IndexFormat = "my-index-{0:yyyy.MM.dd}",
                })
                .CreateLogger();
    
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
        }
}