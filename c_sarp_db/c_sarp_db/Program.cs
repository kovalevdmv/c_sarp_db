using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace ConsoleAppDB
{
    class Program
    {
        static void Main(string[] args)
        {




            using (var connection = new SqliteConnection(@"Data Source=hello.db"))
            {

                //SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    return;

                }

                var command = connection.CreateCommand();
                command.CommandText = "create table if not exists files (id INTEGER PRIMARY KEY, pach TEXT, hash TEXT);";
                command.ExecuteNonQuery();
                //connection.Close();

                command.CommandText = "delete from files;";
                command.ExecuteNonQuery();


                int count = 0;
                for (int i = 1; i < 1000000; i++)
                {
                    command.CommandText = @"insert into files (id,pach,hash) values ($id,$pach,$hash);";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("$id", i);
                    command.Parameters.AddWithValue("$pach", "123");
                    command.Parameters.AddWithValue("$hash", "1234");
                    command.ExecuteNonQuery();
                    count++;
                    if (count == 1000)
                    {
                        count = 0;
                        Console.WriteLine(i);
                    }
                }

                connection.Close();

                /*
                        @"
                SELECT name
                FROM user
                WHERE id = $id
            ";
                        command.Parameters.AddWithValue("$id", id);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var name = reader.GetString(0);

                                Console.WriteLine($"Hello, {name}!");
                            }

                        }

                        */
            }


            //Console.ReadKey();
        }
    }
}
