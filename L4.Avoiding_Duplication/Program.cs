using System.Data;
using Dapper;
using Npgsql;
using static L4.Avoiding_Duplication.Answers;

// 2nd question in the end of chapter
Console.WriteLine(Negate(IsEven)(4));
// 3rd question
List<int> list = [9, 3, 88, 1726, 7];
list.OrderBy(x => x).AsList().ForEach(x => Console.Write(x + " ")); // it's a introsort != quicksort
// book approach
Console.WriteLine();
list.QuickSort().ForEach( x => Console.Write(x + " "));
Console.WriteLine();
// original list
list.ForEach( x => Console.Write(x + " "));

// Using HOFs to avoid duplication (sandwich: setup(repeats), our custom logic, teardown(repeats))


public static class ConnectionHelper
{
    public static R Connect<R>(string connString, Func<IDbConnection, R> f) =>
        FDisposable.Using(new NpgsqlConnection(connString),
            connection => { connection.Open(); return f(connection); }); // railway-oriented paradigm
    /*{
        using var conn = new NpgsqlConnection(connString);
        conn.Open();
        return f(conn);
    }*/
}

public class DbLogger(string connString)
{
    public void Log(LogMessage message) =>
        ConnectionHelper.Connect(connString, c =>
            c.Execute("sp_create_log", message, commandType: CommandType.StoredProcedure));

    public IEnumerable<LogMessage> GetLogs(DateTime since) =>
        ConnectionHelper.Connect(connString,
            c => c.Query<LogMessage>(
                @"SELECT * FROM logs WHERE timestamp > @since",
                new { since }));
}

public class LogMessage
{
    public int Id { get; set; }
    public string Message { get; set; } = "";
    public DateTime Timestamp { get; set; }
}

// Turning the using statement into a HOF

public static class FDisposable
{
    public static R Using<TDisp, R>(TDisp disposable, Func<TDisp, R> f) where TDisp : IDisposable
    {
        using (disposable) return f(disposable);
    }
    
    // answer on the 5th question
    public static R Using<TDisp, R>(Func<TDisp> createDisposable, Func<TDisp, R> func) where TDisp : IDisposable
    {
        using var disp = createDisposable(); return func(disp);
    }
}

/*So, HOFs also have some drawbacks:
     You’ve increased stack use. There’s a performance impact, but it’s negligible.
     Debugging the application will be a bit more complex because of the callbacks.*/