using contactsApi.Repository;
using contactsApi.Services;

namespace contactsApi;

public static class ServiceSetup
{

    public static void SetupServices(this IServiceCollection services)
    {
        registerRepository(services);
        registerServices(services);
    }

    public static void registerRepository(IServiceCollection services)
    {
        services.AddScoped<IContactRepository, ContactRepository>();
    }

    public static void registerServices(IServiceCollection services)
    {
        services.AddTransient<IContactsService, ContactService>();
    }
}