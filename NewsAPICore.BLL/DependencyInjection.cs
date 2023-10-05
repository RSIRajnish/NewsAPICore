using NewsAPICore.BLL.Utilities.AutoMapperProfiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsAPICore.BLL.Services.IServices;
using NewsAPICore.BLL.Services;

namespace NewsAPICore.BLL;

public static class DependencyInjection
{
    public static void RegisterBLLDependencies(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddAutoMapper(typeof(AutoMapperProfiles));
        services.AddScoped<INewsService, NewsService>();
    }
}