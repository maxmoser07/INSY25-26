using System.Diagnostics;

namespace performancetest;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text;
public class MySQLManager
{
    static string connStr = "Server=localhost;Database=entries;Port=32768;User ID=root;Password=insy;";
    public static void Start()
    {
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            Random random = new Random();
            Stopwatch sw =  new Stopwatch();
            sw.Start();
            for (int i = 0; i < 100; i++)
            {
                MySqlCommand cmd = new MySqlCommand(@"SELECT Value FROM entries WHERE Id = " + random.Next(0,1000000) + ";", conn);
                MySqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    string str = rd.GetString(0);
                    rd.Close();
                    cmd =  new MySqlCommand("SELECT * FROM entries WHERE Value = '" + str +  "';", conn);
                    rd = cmd.ExecuteReader();
                    rd.Read();
                    rd.Close();
                }
                else
                {
                    Console.WriteLine("asfdf");
                }
                
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }
    }

    public static void Count()
    {
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            Random random = new Random();
            Stopwatch sw =  new Stopwatch();
            sw.Start();
            for (int i = 0; i < 100; i++)
            {
                string randomletter = Insert.RandomString(random, 5) + "%";
                //Console.WriteLine(randomletter);
                MySqlCommand cmd =  new MySqlCommand("SELECT count(Id) FROM entries WHERE Value like @rand", conn);
                cmd.Parameters.AddWithValue("@rand", randomletter);
                MySqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                //    Console.WriteLine(rd.GetInt32(0));
                }else{
                //    Console.WriteLine("ALARM");
                }
                rd.Close();
                
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }
    }

    public static void SearchInts()
    {
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            Random random = new Random();
            Stopwatch sw =  new Stopwatch();
            sw.Start();
            for (int i = 0; i < 100; i++)
            {
                int randomletter = random.Next(1,101);
                //Console.WriteLine(randomletter);
                MySqlCommand cmd =  new MySqlCommand("SELECT Nr FROM entries WHERE Nr = @rand", conn);
                cmd.Parameters.AddWithValue("@rand", randomletter);
                MySqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                        //Console.WriteLine(rd.GetInt32(0));
                }else{
                        Console.WriteLine("ALARM");
                }
                rd.Close();
                
            }
            sw.Stop();
                Console.WriteLine(sw.Elapsed);
        }
    }
}