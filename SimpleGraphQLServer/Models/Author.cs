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
}