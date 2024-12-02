using System;

namespace SimpleGraphQLServer_V2;

public class Program
{
    public static Task Main(string[] args) => Host
        .CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>())
        .Build()
        .RunAsync();
}
