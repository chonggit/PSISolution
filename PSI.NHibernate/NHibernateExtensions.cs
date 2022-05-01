using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Cfg.Loquacious;
using NHibernate.Driver;
using NHibernate.Dialect;
using NHibernate.Connection;

namespace PSI.NHibernate
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
        public static void AddNHibernate(this IServiceCollection services, Action<DbIntegrationConfigurationProperties> dbConfig)
        {
            services.AddSingleton<ISessionFactory>((servicProvider) =>
            {
                return GetConfiguration(dbConfig).BuildSessionFactory();
            });

            services.AddScoped<ISession>((servicProvider) =>
            {
                ISessionFactory factory = servicProvider.GetRequiredService<ISessionFactory>();

                return factory.OpenSession();
            });
        }

        /// <summary>
        /// SQLite NHibernate
        /// </summary>
        /// <param name="services">ServiceCollection</param>
        /// <param name="connectionString">数据库连接字符串</param>
        public static void AddNHibernateSQLite(this IServiceCollection services, string connectionString)
        {
            services.AddNHibernate(db =>
            {
                db.ConnectionProvider<DriverConnectionProvider>();
                db.Driver<SQLite20Driver>();
                db.Dialect<SQLiteDialect>();
                db.ConnectionString = connectionString;
#if DEBUG
                db.LogSqlInConsole = true;
#endif
            });
        }

        /// <summary>
        /// 获取 NHibernate 配置
        /// </summary>
        /// <param name="dbConfig">数据配置</param>
        /// <returns>NHibernate 配置</returns>
        public static Configuration GetConfiguration(Action<DbIntegrationConfigurationProperties> dbConfig)
        {
            var config = new Configuration();

            config.DataBaseIntegration(dbConfig);

            config.AddMapping(CreateHbmMapping());

            return config;
        }

        /// <summary>
        ///获取创建数据库映射
        /// </summary>
        /// <returns>Hbm 数据库映射</returns>
        static HbmMapping CreateHbmMapping()
        {
            Type[] types = typeof(NHibernateExtensions).Assembly.GetTypes()
                       .Where(t => t.IsClass && typeof(IConformistHoldersProvider).IsAssignableFrom(t))
                       .ToArray();
            var modelMapper = new ModelMapper();

            modelMapper.AddMappings(types);

            HbmMapping mapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();

            return mapping;
        }
    }
}
