using SimpleGraphQLServer_V2;
using GraphQL.Server;
using GraphQL;
using GraphQL.Types;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddGraphQL(builder => builder
            .AddSystemTextJson()
            .AddSchema<BookSchema>()
            .AddSchema<AuthorSchema>()
            .AddGraphTypes(typeof(BookSchema).Assembly)
            .AddGraphTypes(typeof(AuthorSchema).Assembly)
        );
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseGraphQL<BookSchema>("/graphql/book");
        app.UseGraphQL<AuthorSchema>("/graphql/author");
        app.UseGraphQLPlayground();
    }
}
