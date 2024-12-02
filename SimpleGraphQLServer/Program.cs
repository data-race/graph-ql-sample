using SimpleGraphQLServer.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<BookType>()
    .AddType<AuthorType>()
    .AddDataLoader<AuthorDataLoader>();

var app = builder.Build();

app.MapGraphQL("/graphql");
app.Run();
