using contactsApi.Repository;

namespace contactsApi;

public static class ServiceSetup
{

    public static void SetupServices(this IServiceCollection services)
    {
        registerRepository(services);
    }

    public static void registerRepository(IServiceCollection services)
    {
        services.AddScoped<IContactRepository, ContactRepository>();
    }
}