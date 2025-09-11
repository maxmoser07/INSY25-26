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

            int numberOfEntries = 1000000;
            var entries = new List<string>();

            // Generate all random strings
            /*for (int i = 0; i < numberOfEntries; i++)
            {
                entries.Add(GenerateRandomString(10));
            }*/

            // Insert all entries at once
            //await InsertEntriesAsync(connectionString, entries);

            // Example search
            watch.Start();
            for (int i = 0; i < 100; i++)
            {
                var searchTerm = GenerateRandomString(1); // change to whatever you want to search
                var results = await SearchEntriesAsync(connectionString, searchTerm);

                Console.WriteLine($"Found {results.Count} entries containing '{searchTerm}':");
                /*foreach (var (id, value) in results)
                {
                    Console.WriteLine($"ID: {id}"); // Print the ID
                }*/
            }

            watch.Stop();
            Console.WriteLine($"Search completed in {watch.ElapsedMilliseconds} ms");
        }

        static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            char[] stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        static async Task InsertEntriesAsync(string connectionString, List<string> entries)
        {
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            // Build SQL with multiple parameters
            string sql = "INSERT INTO entries (`VALUE`) VALUES ";
            sql += string.Join(",", entries.Select((_, idx) => $"(@val{idx})"));

            using var cmd = new MySqlCommand(sql, connection);

            // Add all parameters
            for (int i = 0; i < entries.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@val{i}", entries[i]);
            }

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            Console.WriteLine($"{rowsAffected} row(s) inserted.");
        }

        static async Task<List<(long Id, string Value)>> SearchEntriesAsync(string connectionString, string searchTerm)
        {
            var results = new List<(long, string)>();

            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();
        
            string sql = "SELECT id, `VALUE` FROM entries WHERE `VALUE` LIKE @search";

            using var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@search", "%" + searchTerm + "%");

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
