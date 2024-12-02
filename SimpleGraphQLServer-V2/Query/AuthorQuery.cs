using System;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using SimpleGraphQLServer_V2.Models;

namespace SimpleGraphQLServer_V2.Query;

public class AuthorQuery: ObjectGraphType
{
    public AuthorQuery()
    {
        Name = "AuthorQuery";
         AddField(new FieldType
        {
            Name = "author",
            Type = typeof(AuthorType),
            Arguments = new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
            Resolver = new FuncFieldResolver<object>(context =>
            {
                var id = context.GetArgument<int>("id");
                return QueryHelper.AuthorById(id);
            })
        });
    }
}
