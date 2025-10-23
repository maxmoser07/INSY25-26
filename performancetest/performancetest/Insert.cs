namespace performancetest;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text;
using System.IO;
public class Insert
{
    public static void Start()
    {
        string connStr = "server=localhost;user=root;password=insy;database=DB;";
        int totalRecords = 1_000_000;
        int batchSize = 1000;

        using (var conn = new MySqlConnection(connStr))
        {
            using (var writer = new StreamWriter("randomStrings.txt", false, Encoding.UTF8))
            {
                conn.Open();
                new MySqlCommand("TRUNCATE entries", conn).ExecuteNonQuery();
                var random = new Random();

                for (int i = 0; i < totalRecords; i += batchSize)
                {
                    var sb = new StringBuilder();
                    sb.Append("INSERT INTO entries (Value, Nr) VALUES ");

                    var values = new List<string>();
                    for (int j = 0; j < batchSize && (i + j) < totalRecords; j++)
                    {
                        string val = RandomString(random, 10);
                        int nr = random.Next(1, 101); // random 1–5
                        values.Add($"('{val}', {nr})");

                        // also log to file (optional)
                        writer.WriteLine($"{i + j},{val},{nr}");
                    }

                    sb.Append(string.Join(",", values));
                    sb.Append(";");

                    using (var cmd = new MySqlCommand(sb.ToString(), conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    Console.WriteLine($"Inserted {Math.Min(i + batchSize, totalRecords)} of {totalRecords}");
                }
            }
        }

        Console.WriteLine("Done!");
    }


    public static string RandomString(Random random, int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var result = new char[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = chars[random.Next(chars.Length)];
        }
        return new string(result);
    }
}