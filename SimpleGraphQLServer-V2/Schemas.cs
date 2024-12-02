using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using SimpleGraphQLServer_V2.Query;

namespace SimpleGraphQLServer_V2;

public class BookSchema : Schema
{
    public BookSchema(IServiceProvider provider, BookQuery query) : base(provider)
    {
        Query = query;
    }
}

public class AuthorSchema : Schema
{
    public AuthorSchema(IServiceProvider provider, AuthorQuery query) : base(provider)
    {
        Query = query;
    }
}


