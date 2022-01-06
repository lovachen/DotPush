using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotPush.Web.Core
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 配置文件扩展
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            var config = new TConfig();

            configuration.Bind(config);

            services.AddSingleton(config);

            return config;
        }
    }
}