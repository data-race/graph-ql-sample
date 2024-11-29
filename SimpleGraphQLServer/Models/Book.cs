using HotChocolate.Resolvers;

namespace SimpleGraphQLServer.Models
{
    [GraphQLDescription("A book")]
    public class Book
    {
        [GraphQLDescription("The unique identifier of the book")]
        [ID]
        public int Id { get; set; }

        [GraphQLDescription("The title of the book")]
        public string Title { get; set; }

        [GraphQLDescription("The rating of the book")]
        public float Rating { get; set; }

        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public Book(int id, string title, float rating, int authorId)
        {
            Id = id;
            Title = title;
            Rating = rating;
            AuthorId = authorId;
        }
    }

    public class BookType: ObjectType<Book>
    {
        protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
        {
            descriptor.Field(b=>b.Author)
                .Resolve(context => {
                    Book book = context.Parent<Book>();
                    return QueryHelper.AuthorById(book.AuthorId);
                });
        }
    }
}