﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Providers;
using Orleans.Runtime;
using System;
using Orleans.Storage;

namespace Orleans.Persistence
{
    public static class Extensions
    {
        public static ISiloBuilder AddRedisGrainStorageAsDefault(this ISiloBuilder builder, Action<RedisStorageOptions> configureOptions)
        {
            return builder.AddRedisGrainStorage(ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME, configureOptions);
        }

        public static ISiloBuilder AddRedisGrainStorage(this ISiloBuilder builder, string name, Action<RedisStorageOptions> configureOptions)
        {
            return builder.ConfigureServices(services => services.AddRedisGrainStorage(name, configureOptions));
        }

        public static ISiloBuilder AddRedisGrainStorageAsDefault(this ISiloBuilder builder, Action<OptionsBuilder<RedisStorageOptions>> configureOptions = null)
        {
            return builder.AddRedisGrainStorage(ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME, configureOptions);
        }

        public static ISiloBuilder AddRedisGrainStorage(this ISiloBuilder builder, string name, Action<OptionsBuilder<RedisStorageOptions>> configureOptions = null)
        {
            return builder.ConfigureServices(services => services.AddRedisGrainStorage(name, configureOptions));
        }

        public static ISiloHostBuilder AddRedisGrainStorageAsDefault(this ISiloHostBuilder builder, Action<RedisStorageOptions> configureOptions)
        {
            return builder.AddRedisGrainStorage(ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME, configureOptions);
        }

        public static ISiloHostBuilder AddRedisGrainStorageAsDefault(this ISiloHostBuilder builder, Action<OptionsBuilder<RedisStorageOptions>> configureOptions = null)
        {
            return builder.AddRedisGrainStorage(ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME, configureOptions);
        }

        public static ISiloHostBuilder AddRedisGrainStorage(this ISiloHostBuilder builder, string name, Action<RedisStorageOptions> configureOptions)
        {
            return builder.ConfigureServices(services => services.AddRedisGrainStorage(name, configureOptions));
        }

        public static ISiloHostBuilder AddRedisGrainStorage(this ISiloHostBuilder builder, string name, Action<OptionsBuilder<RedisStorageOptions>> configureOptions = null)
        {
            return builder.ConfigureServices(services => services.AddRedisGrainStorage(name, configureOptions));
        }

        public static IServiceCollection AddRedisGrainStorageAsDefault(this IServiceCollection services, Action<RedisStorageOptions> configureOptions)
        {
            return services.AddRedisGrainStorage(ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME, ob => ob.Configure(configureOptions));
        }

        public static IServiceCollection AddRedisGrainStorage(this IServiceCollection services, string name, Action<RedisStorageOptions> configureOptions)
        {
            return services.AddRedisGrainStorage(name, ob => ob.Configure(configureOptions));
        }

        public static IServiceCollection AddRedisGrainStorageAsDefault(this IServiceCollection services, Action<OptionsBuilder<RedisStorageOptions>> configureOptions = null)
        {
            return services.AddRedisGrainStorage(ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME, configureOptions);
        }

        public static IServiceCollection AddRedisGrainStorage(this IServiceCollection services, string name,
            Action<OptionsBuilder<RedisStorageOptions>> configureOptions = null)
        {
            configureOptions?.Invoke(services.AddOptions<RedisStorageOptions>(name));
            services.ConfigureNamedOptionForLogging<RedisStorageOptions>(name);
            services.TryAddSingleton<IGrainStorage>(sp => sp.GetServiceByName<IGrainStorage>(ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME));
            services.AddTransient<IConfigurationValidator>(sp => new RedisStorageOptionsValidator(sp.GetRequiredService<IOptionsMonitor<RedisStorageOptions>>().Get(name), name));
            return services.AddSingletonNamedService<IGrainStorage>(name, RedisGrainStorageFactory.Create)
                           .AddSingletonNamedService<ILifecycleParticipant<ISiloLifecycle>>(name, (s, n) => (ILifecycleParticipant<ISiloLifecycle>)s.GetRequiredServiceByName<IGrainStorage>(n));
        }
    }
}
