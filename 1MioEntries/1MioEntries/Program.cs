using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace _1MioEntries
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            var connectionString = "Server=localhost;Database=entries;Port=32768;User ID=root;Password=insy;";

            int numberOfEntries = 1_000_000;
            var entries = new List<string>();

            // Generate 1M random 5-digit numbers
            for (int i = 0; i < numberOfEntries; i++)
            {
                entries.Add(GenerateRandomNumberString(5));
            }

            // Insert in batches (e.g. 10,000 rows at once)
            await InsertEntriesAsync(connectionString, entries, batchSize: 10_000);

            // Example search: 100 random numbers
            watch.Start();
            var rnd = new Random();

            for (int i = 0; i < 100; i++)
            {
                var searchTerm = GenerateRandomNumberString(5);
                var results = await SearchEntriesAsync(connectionString, searchTerm);
                Console.WriteLine($"Search {i + 1}: Found {results.Count} entries containing '{searchTerm}'");
            }

            watch.Stop();
            Console.WriteLine($"100 searches completed in {watch.ElapsedMilliseconds} ms");
        }

        // Generate a random numeric string
        static string GenerateRandomNumberString(int length)
        {
            var rnd = new Random();
            char[] digits = new char[length];
            for (int i = 0; i < length; i++)
            {
                digits[i] = (char)('0' + rnd.Next(0, 10));
            }
            return new string(digits);
        }

        // Insert entries in batches
        static async Task InsertEntriesAsync(string connectionString, List<string> entries, int batchSize = 10_000)
        {
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            for (int offset = 0; offset < entries.Count; offset += batchSize)
            {
                var batch = entries.Skip(offset).Take(batchSize).ToList();

                string sql = "INSERT INTO entries (`VALUE`) VALUES " +
                             string.Join(",", batch.Select((_, idx) => $"(@val{idx})"));

                using var cmd = new MySqlCommand(sql, connection);

                for (int i = 0; i < batch.Count; i++)
                {
                    cmd.Parameters.AddWithValue($"@val{i}", batch[i]);
                }

                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                Console.WriteLine($"Inserted {rowsAffected} rows (offset {offset})");
            }
        }

        // Search for a number string
        static async Task<List<(long Id, string Value)>> SearchEntriesAsync(string connectionString, string searchTerm)
        {
            var results = new List<(long, string)>();

            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            string sql = "SELECT id, `VALUE` FROM entries WHERE `VALUE` = @search";

            using var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@search", searchTerm);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                long id = reader.GetInt64(0);
                string value = reader.GetString(1);
                results.Add((id, value));
            }

            return results;
        }
    }
}
