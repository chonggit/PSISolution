using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

namespace PSI.Data
{
    /// <summary>
    /// Service Collection Extensoions
    /// </summary>
    public static class NHibernateExtensions
    {
        /// <summary>
        /// 注入 NHibernate SessionFactory Session 到 ServiceCollection
        /// </summary>
        /// <param name="services">ServiceCollection</param>
        /// <param name="dbConfig">NHibernate 数据库配置</param>
        public static void AddNHibernate(this IServiceCollection services, Action<Configuration> configurate)
        {
            services.AddNHibernate((configuration, serviceProvider) =>
            {
                configurate(configuration);
            });
        }

        /// <summary>
        /// 注入 NHibernate SessionFactory Session 到 ServiceCollection
        /// </summary>
        /// <param name="services">ServiceCollection</param>
        /// <param name="dbConfig">NHibernate 数据库配置</param>
        public static void AddNHibernate(this IServiceCollection services, Action<Configuration, IServiceProvider> configurate)
        {
            services.AddSingleton((servicProvider) =>
            {
                var configuration = new Configuration();

                configurate(configuration, servicProvider);

                return configuration;
            });

            services.AddSingleton((servicProvider) => servicProvider.GetRequiredService<Configuration>().BuildSessionFactory());
            services.AddScoped((servicProvider) => servicProvider.GetRequiredService<ISessionFactory>().OpenSession());
            services.AddScoped<IDbSession, NHDbSession>();
        }

        /// <summary>
        /// 使用 Sqlite 数据库
        /// </summary>
        /// <param name="config">配置</param>
        /// <param name="connectionString">数据库连接字符串</param>
        public static void UseSqlite(this Configuration config, string connectionString)
        {
            config.DataBaseIntegration(db =>
            {
                db.ConnectionProvider<DriverConnectionProvider>();
                db.Driver<SQLite20Driver>();
                db.Dialect<SQLiteDialect>();
                db.ConnectionString = connectionString;
#if DEBUG
                db.LogSqlInConsole = true;
#endif
            });

            AddMappings(config, "PSI.Data.Mappings.Sqlite");
        }

        /// <summary>
        /// 获取映射配置类
        /// </summary>
        /// <param name="space">映射配置类所在命名空间</param>
        /// <returns>映射配置类</returns>
        static Type[] GetMappingTypes(string space)
        {
            return typeof(NHibernateExtensions).Assembly.GetTypes()
                       .Where(t => t.IsClass
                       && typeof(IConformistHoldersProvider).IsAssignableFrom(t)
                       && t.Namespace == space)
                       .ToArray();
        }

        /// <summary>
        /// 添加关系映射到配置
        /// </summary>
        /// <param name="config">配置</param>
        /// <param name="space">映射配置类所在命名空间</param>
        static void AddMappings(Configuration config, string space)
        {
            var modelMapper = new ModelMapper();

            modelMapper.AddMappings(GetMappingTypes(space));

            HbmMapping mapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();

            config.AddMapping(mapping);
        }
    }
}
