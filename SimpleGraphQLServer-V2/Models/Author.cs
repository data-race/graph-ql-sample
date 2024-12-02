using System;
using GraphQL.Types;
using SimpleGraphQLServer_V2.Query;

namespace SimpleGraphQLServer_V2.Models {
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public IEnumerable<Book>? Books { get; set; }

        public Author(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }

    public class AuthorType: ObjectGraphType<Author>
    {
        public AuthorType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("The Id of the Author.");
            Field(x => x.Name).Description("The Name of the Author.");
            Field(x => x.Age).Description("The Age of the Author.");
            Field<ListGraphType<BookType>>("Books").Description("The Books of the Author.")
                .Resolve(context => {
                    // query books from sql using author's id
                    var authorId = context.Source.Id;
                    return QueryHelper.BooksByAuthorId(authorId);
                });
        }
    }
}


