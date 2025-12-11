using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Location;

class Program
{
    static void Main()
    {
        string connectionString =
            "server=localhost;user=root;password=insy;database=geodata;port=32768";

        double minLat = 999, maxLat = -999, minLon = 999, maxLon = -999;

        // 1. Get bounding box from lat/lon columns
        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();

            string sql = @"
                SELECT MIN(lat), MAX(lat), MIN(lon), MAX(lon)
                FROM tipps
                WHERE lat IS NOT NULL AND lon IS NOT NULL;
            ";

            using (var cmd = new MySqlCommand(sql, conn))
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    minLat = reader.GetDouble(0);
                    maxLat = reader.GetDouble(1);
                    minLon = reader.GetDouble(2);
                    maxLon = reader.GetDouble(3);
                }
            }
        }

        var rand = new Random();
        var stopwatch = Stopwatch.StartNew();

        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();

            for (int i = 1; i <= 100; i++)
            {
                // Generate valid random lat/lon within the bounding box
                double randLat = minLat + rand.NextDouble() * (maxLat - minLat);
                double randLon = minLon + rand.NextDouble() * (maxLon - minLon);

                // Query using POINT column
                string sql = @"
                    SELECT COUNT(*)
                    FROM tipps
                    WHERE ST_Distance_Sphere(
                        location,
                        ST_SRID(POINT(@lon, @lat), 4326)
                    ) <= 1000;
                ";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@lat", randLat);
                    cmd.Parameters.AddWithValue("@lon", randLon);

                    long count = (long)cmd.ExecuteScalar();

                    //Console.WriteLine(
                     //   $"Random #{i:000}: ({randLat:F6}, {randLon:F6}) → {count} tipps within 1 km"
                    //);
                }
            }
        }

        stopwatch.Stop();
        Console.WriteLine($"\nTotal time: {stopwatch.ElapsedMilliseconds} ms");
    }
}
