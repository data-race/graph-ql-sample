using SimpleGraphQLServer.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<BookType>();

var app = builder.Build();

app.MapGraphQL("/graphql");
app.Run();