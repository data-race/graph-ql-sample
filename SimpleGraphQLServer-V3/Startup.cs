using SimpleGraphQLServer_V3;
using GraphQL.Server;
using GraphQL;
using GraphQL.Types;
using SimpleGraphQLServer_V3.Dataloader;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<AuthorsDataLoader>();
        services.AddGraphQL(builder => builder
            .AddSystemTextJson()
            .AddSchema<MainSchema>()
            .AddGraphTypes(typeof(MainSchema).Assembly)
        );
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseGraphQL<MainSchema>("/graphql");
        app.UseGraphQLPlayground();
    }
}
