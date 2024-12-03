using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using SimpleGraphQLServer_V3.Query;

namespace SimpleGraphQLServer_V3;

public class MainSchema : Schema
{
    public MainSchema(IServiceProvider provider, Query.Query query) : base(provider)
    {
        Query = query;
    }
}



