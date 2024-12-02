namespace SimpleGraphQLServer.Models
{
    [GraphQLDescription("An author")]
    public class Author
    {
        [GraphQLDescription("The unique identifier of the author")]
        [ID]
        public int Id { get; set; }

        [GraphQLDescription("The name of the author")]
        public string Name { get; set; }

        [GraphQLDescription("The age of the author")]
        public int Age { get; set; }

        public Author(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }

    public class AuthorType: ObjectType<Author>
    {
        protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
        {
            descriptor.Field(t => t.Id)
                .Type<NonNullType<IdType>>();

            descriptor.Field(t => t.Name)
                .Type<NonNullType<StringType>>();

            descriptor.Field(t => t.Age)
                .Type<NonNullType<IntType>>();
        }
    }

    public class AuthorDataLoader : BatchDataLoader<int, Author>
    {
        public AuthorDataLoader(IBatchScheduler batchScheduler, DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
        }

        protected override async Task<IReadOnlyDictionary<int, Author>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Loading authors by ids: {string.Join(", ", keys)}");
            return QueryHelper.AuthorsByIds(keys);
        }
    }
}