using Asp.Versioning;

namespace CoreBaking.API.Bootstraping;

public static class ApplicationServicesExtensions
{
    public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddServiceDefaults();
        builder.Services.AddOpenApi();
        builder.Services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-version"),
                new MediaTypeApiVersionReader());
        });
        return builder;
    }
    public static void AddServiceDefaults(this IApplicationBuilder builder)
    {

    }
}
