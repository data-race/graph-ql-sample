using System.Data.SQLite;
using SimpleGraphQLServer.Models;

public class Query
{
    public IEnumerable<Book> Books()
    {
        return QueryHelper.Books();
    }

    public Book? BookById([ID] int id)
    {
        return QueryHelper.BookById(id);
    }

    // get books with higher rating than the given rating
    public IEnumerable<Book> BooksWithRatingGreaterThan(float rating)
    {
        return QueryHelper.BooksWithRatingGreaterThan(rating);
    }
}

public static class QueryHelper {

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
}