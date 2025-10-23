using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace where_clause;

class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=localhost;user=root;password=insy;database=scott_big";
            int runs = 10; // number of repetitions
            Random rand = new Random();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connected to database.\n");

                double totalNonOpt = 0, totalOpt = 0, totalIndex = 0;

                for (int i = 0; i < runs; i++)
                {
                    // Pick a random year between 1980 and 1990
                    int year = rand.Next(1980, 1991);
                    string start = $"{year}-01-01";
                    string end = $"{year + 1}-01-01";

                    // Build queries dynamically
                    string nonOptimizedQuery = $@"
                        SELECT * FROM emps
                        WHERE YEAR(HIREDATE) = {year};
                    ";

                    string optimizedQuery = $@"
                        SELECT * FROM emps
                        WHERE HIREDATE >= '{start}' AND HIREDATE < '{end}';
                    ";

                    string indexedQuery = $@"
                        SELECT * FROM emps
                        WHERE YEAR(HIREDATE) = {year};
                    ";

                    // Run each query once with different years
                    double t1 = MeasureQueryTime(connection, nonOptimizedQuery);
                    double t2 = MeasureQueryTime(connection, optimizedQuery);
                    double t3 = MeasureQueryTime(connection, indexedQuery);

                    totalNonOpt += t1;
                    totalOpt += t2;
                    totalIndex += t3;

                    Console.WriteLine($"Run {i + 1}/{runs} - Year: {year}");
                    Console.WriteLine($"  Non-optimized: {t1:F2} ms");
                    Console.WriteLine($"  Optimized: {t2:F2} ms");
                    Console.WriteLine($"  With Index: {t3:F2} ms\n");
                }

                // Average over all runs
                Console.WriteLine("=====================================================");
                Console.WriteLine($"{"Query Type",-25} {"Average Time (ms)",20}");
                Console.WriteLine("=====================================================");
                Console.WriteLine($"{"Non-Optimized",-25} {totalNonOpt / runs,20:F3}");
                Console.WriteLine($"{"Optimized",-25} {totalOpt / runs,20:F3}");
                Console.WriteLine($"{"With Functional Index",-25} {totalIndex / runs,20:F3}");
                Console.WriteLine("=====================================================");
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
                    while (reader.Read()) { /* simulate reading rows */ }
                }
                stopwatch.Stop();
            }
            return stopwatch.Elapsed.TotalMilliseconds;
        }
    }
    /*
     no index:
        Non-Optimized                          173,558
        Optimized                              179,007
    with index on hiredate
        Non-Optimized                          172,754
        Optimized                              136,042
    with index on year(hiredate)
        Non-Optimized                          123,120
        Optimized                              199,948
    */