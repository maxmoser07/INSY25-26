using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace where_clause;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "server=localhost;user=root;password=insy;database=scottnew";

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            // Example 1: Non-optimized WHERE clause
            string nonOptimizedQuery = @"
                    SELECT * FROM emps
                    WHERE YEAR(HIREDATE) = 1981;
                ";

            // Example 2: Optimized WHERE clause
            string optimizedQuery = @"
                    SELECT * FROM emps
                    WHERE HIREDATE >= '1981-01-01' AND HIREDATE < '1982-01-01';
                ";

            Console.WriteLine("Running non-optimized query...");
            double nonOptimizedTime = MeasureQueryTime(connection, nonOptimizedQuery);
            Console.WriteLine($"Non-optimized query took: {nonOptimizedTime} ms\n");

            Console.WriteLine("Running optimized query...");
            double optimizedTime = MeasureQueryTime(connection, optimizedQuery);
            Console.WriteLine($"Optimized query took: {optimizedTime} ms\n");

            Console.WriteLine($"Difference: {nonOptimizedTime - optimizedTime} ms faster when optimized.");
        }
    }

    static double MeasureQueryTime(MySqlConnection connection, string query)
    {
        Stopwatch stopwatch = new Stopwatch();
        using (var command = new MySqlCommand(query, connection))
        {
            stopwatch.Start();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read()) ;
            }
            stopwatch.Stop();
        }
        return stopwatch.Elapsed.TotalMilliseconds;
    }
}