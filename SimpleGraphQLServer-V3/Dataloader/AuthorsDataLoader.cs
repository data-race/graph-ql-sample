using System;
using SimpleGraphQLServer_V3.Models;
using GraphQL.DataLoader;
using SimpleGraphQLServer_V3.Query;

namespace SimpleGraphQLServer_V3.Dataloader;

public class AuthorsDataLoader : DataLoaderBase<int, Author>
{
    protected override Task FetchAsync(IEnumerable<DataLoaderPair<int, Author>> list, CancellationToken cancellationToken)
    {
        var authorIds = list.Select(x => x.Key).ToList();
        var authors = QueryHelper.AuthorsByIds(authorIds);
        foreach (var pair in list) {
            pair.SetResult(authors[pair.Key]);
        }

        return Task.CompletedTask;
    }
}
