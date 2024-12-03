using System;
using GraphQL.Types;
using SimpleGraphQLServer_V3.Dataloader;

namespace SimpleGraphQLServer_V3.Models {
    public class Book {
        public int Id { get; set; }
        public string Title { get; set; }
        public float Rating { get; set; }

        public int AuthorId { get; set; }

        public Author? Author { get; set; }

        public Book(int id, string title, float rating, int authorId) {
            Id = id;
            Title = title;
            Rating = rating;
            AuthorId = authorId;
        }
    }

    public class BookType: ObjectGraphType<Book> {
        public BookType() {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("The Id of the Book.");
            Field(x => x.Title).Description("The Title of the Book.");
            Field(x => x.Rating).Description("The Rating of the Book.");
            Field<AuthorType>("Author").Description("The Author of the Book.")
                .Resolve(context => {
                    // query author from sql using book's author id
                    var authorId = context.Source.AuthorId;
                    var loader = context.RequestServices!.GetRequiredService<AuthorsDataLoader>();
                    return loader.LoadAsync(authorId);
                });
        }
    }
}
