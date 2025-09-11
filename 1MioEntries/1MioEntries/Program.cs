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
            for (int i = 0; i < numberOfEntries; i++)
            {
                entries.Add(GenerateRandomString(10));
            }

            // Insert all entries at once
            //await InsertEntriesAsync(connectionString, entries);

            // Example search
            watch.Start();
            for (int i = 0; i < 100; i++)
            {
                int searchNumber = new Random().Next(1, 6); // pick random Nr between 1–5
                var results = await SearchEntriesAsync(connectionString, searchNumber);

                //Console.WriteLine($"Found {results.Count} entries with Nr = {searchNumber}");
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

            // Build SQL with VALUE and Nr columns
            string sql = "INSERT INTO entries (VALUE, Nr) VALUES ";
            sql += string.Join(",", entries.Select((_, idx) => $"(@val{idx}, @nr{idx})"));

            using var cmd = new MySqlCommand(sql, connection);

            var rnd = new Random();

            // Add all parameters
            for (int i = 0; i < entries.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@val{i}", entries[i]);
                cmd.Parameters.AddWithValue($"@nr{i}", rnd.Next(1, 6)); // random number 1–5
            }

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            Console.WriteLine($"{rowsAffected} row(s) inserted.");
        }


        static async Task<List<(long Id, string Value, int Nr)>> SearchEntriesAsync(string connectionString, int number)
        {
            var results = new List<(long, string, int)>();

            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            string sql = "SELECT id, `VALUE`, Nr FROM entries WHERE Nr = @number";

            using var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@number", number);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                long id = reader.GetInt64(0);
                string value = reader.GetString(1);
                int nr = reader.GetInt32(2);
                results.Add((id, value, nr));
            }

            return results;
        }

    }
}
