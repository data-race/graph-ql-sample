using System.Data.SQLite;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using SimpleGraphQLServer_V2.Models;

namespace SimpleGraphQLServer_V2.Query;

public class BookQuery : ObjectGraphType
{
    public BookQuery()
    {
        Name = "BookQuery";
        AddField(new FieldType
        {
            Name = "books",
            Type = typeof(ListGraphType<BookType>),
            Resolver = new FuncFieldResolver<object>(context => QueryHelper.Books())
        });

        // book by id
        AddField(new FieldType
        {
            Name = "book",
            Type = typeof(BookType),
            Arguments = new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
            Resolver = new FuncFieldResolver<object>(context =>
            {
                var id = context.GetArgument<int>("id");
                return QueryHelper.BookById(id);
            })
        });

        // books with rating greater than
        AddField(new FieldType
        {
            Name = "booksWithRatingGreaterThan",
            Type = typeof(ListGraphType<BookType>),
            Arguments = new QueryArguments(new QueryArgument<NonNullGraphType<FloatGraphType>> { Name = "rating" }),
            Resolver = new FuncFieldResolver<object>(context =>
            {
                var rating = context.GetArgument<float>("rating");
                return QueryHelper.BooksWithRatingGreaterThan(rating);
            })
        });
    }
}

public static class QueryHelper
{

    private static string connectionString = "Data Source=sample.db";

    public static IEnumerable<Book> Books()
    {
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();

        using var command = new SQLiteCommand(connection);
        command.CommandText = "SELECT * FROM Books";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            yield return new Book(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetFloat(2),
                reader.GetInt32(3)
            );
        }
    }

    public static Book? BookById(int id)
    {
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();

        using var command = new SQLiteCommand(connection);
        command.CommandText = "SELECT * FROM Books WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Book(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetFloat(2),
                reader.GetInt32(3)
            );
        }

        return null;
    }

    public static IEnumerable<Book> BooksWithRatingGreaterThan(float rating)
    {
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();

        using var command = new SQLiteCommand(connection);
        command.CommandText = "SELECT * FROM Books WHERE Rating > @rating";
        command.Parameters.AddWithValue("@rating", rating);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            yield return new Book(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetFloat(2),
                reader.GetInt32(3)
            );
        }
    }

    public static Author? AuthorById(int id)
    {
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();

        using var command = new SQLiteCommand(connection);
        command.CommandText = "SELECT * FROM Authors WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Author(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetInt32(2)
            );
        }

        return null;
    }

    public static IReadOnlyDictionary<int, Author> AuthorsByIds(IEnumerable<int> ids)
    {
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();

        using var command = new SQLiteCommand(connection);
        command.CommandText = "SELECT * FROM Authors WHERE Id IN (" + string.Join(",", ids) + ")";

        using var reader = command.ExecuteReader();
        var authors = new Dictionary<int, Author>();
        while (reader.Read())
        {
            var author = new Author(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetInt32(2)
            );
            authors.Add(author.Id, author);
        }

        return authors;
    }

    public static IEnumerable<Book> BooksByAuthorId(int authorId)
    {
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();

        using var command = new SQLiteCommand(connection);
        command.CommandText = "SELECT * FROM Books WHERE AuthorId = @authorId";
        command.Parameters.AddWithValue("@authorId", authorId);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            yield return new Book(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetFloat(2),
                reader.GetInt32(3)
            );
        }
    }
}