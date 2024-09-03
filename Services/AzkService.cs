using exam_m2_w4.Interface;
using exam_m2_w4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace exam_m2_w4.Services
{
    public class AzkService : IAzk
    {
        //CreateDatabase
        public static void CreateDatabase()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.DefaultConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommands.CreateDatabase, connection))
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = SqlCommands.CreateDatabase;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //DropDatabase

        public static void DropDatabase()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.DefaultConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommands.DropDatabase, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //CreateTable

        public static void CreateTable()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommands.CreateTable, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //DropTable 

        public static void DropTable()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommands.DropTable, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }



        /*-------------------------------------------------------------------------*/

        public bool CreateAzk(Azk azk)
        {
            try
            {
                int res = 0;
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.CommandText = SqlCommands.InsertAzk;
                        command.Connection = connection;
                        command.Parameters.AddWithValue("azk_id", azk.AzkId);
                        command.Parameters.AddWithValue("name", azk.Name);
                        command.Parameters.AddWithValue("inn", azk.Inn);
                        command.Parameters.AddWithValue("address", azk.Address);
                        command.Parameters.AddWithValue("register_date", azk.RegisterDate);
                        command.Parameters.AddWithValue("register_user_info", azk.RegisterUserInfo);
                        command.Parameters.AddWithValue("phone", azk.Phone);
                        

                        res = command.ExecuteNonQuery();
                    }
                }

                return res > 0;
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<Azk> GetAzk()
        {
            try
            {
                List<Azk> azks = new();
                using (NpgsqlConnection connection = new(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = SqlCommands.ReadAzk;

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Azk azk = new Azk();
                                azk.AzkId = reader.GetInt32(0);
                                azk.Name = reader.GetString(1);
                                azk.Address = reader.GetString(2);
                                azk.Inn=reader.GetString(3);
                                azk.RegisterDate = reader.GetDateTime(4);
                                azk.RegisterUserInfo = reader.GetString(5);
                                azk.Phone = reader.GetString(6);
                                azks.Add(azk);
                            }
                        }
                    }

                    return azks;
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Azk GetAzkById(int azkid)
        {
            try
            {
                Azk azk = new();
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = SqlCommands.ReadAzkById;
                        command.Parameters.AddWithValue("@azk_id", azkid);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                azk.AzkId = reader.GetInt32(0);
                                azk.Name = reader.GetString(1);
                                azk.Address = reader.GetString(2);
                                azk.Inn = reader.GetString(3);
                                azk.RegisterDate = reader.GetDateTime(4);
                                azk.RegisterUserInfo = reader.GetString(5);
                                azk.Phone = reader.GetString(6);
                            }
                            return azk;
                        }
                    }

                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public bool UpdateAzk(Azk azk)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = SqlCommands.UpdateAzk;

                        command.Parameters.AddWithValue("@azk_id", azk.AzkId);
                        command.Parameters.AddWithValue("@name", azk.Name);
                        command.Parameters.AddWithValue("@inn", azk.Inn);
                        command.Parameters.AddWithValue("@address", azk.Address);
                        command.Parameters.AddWithValue("@register_date", azk.RegisterDate);
                        command.Parameters.AddWithValue("@register_user_info", azk.RegisterUserInfo);
                        command.Parameters.AddWithValue("@phone", azk.Phone);
                        

                        int res = command.ExecuteNonQuery();
                        return res > 0;
                    }
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public bool DeleteAzk(int azkid)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = SqlCommands.DeleteAzk;
                        cmd.Parameters.AddWithValue("@azk_id", azkid);
                        int res = cmd.ExecuteNonQuery();
                        return res > 0;
                    }
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }

    }

    //SqlCommands

    file class SqlCommands
    {
        public const string DefaultConnectionString = "Server=localhost;Port=5432;Database=postgres;Username=postgres;Password=1939;";

        public const string ConnectionString = "Server=localhost;Port=5432;Database=monitoring_db;Username=postgres;Password=1939;";

        public const string CreateDatabase = "CREATE DATABASE  monitoring_db";

        public const string DropDatabase = "DROP DATABASE  monitoring_db with(force)";

        public const string CreateTable = @"CREATE TABLE If not exists azk (
                                      azk_id INT PRIMARY KEY, 
                                      name VARCHAR(50) NOT NULL,
                                      inn VARCHAR(50) NOT NULL,
                                      address VARCHAR(100) NOT NULL,
                                      register_date DATE,
                                      register_user_info VARCHAR(100),
                                      phone VARCHAR(100))";

                                      

        public const string DropTable = "DROP TABLE if exists azk";

        public const string InsertAzk = @"Insert into azk(azk_id, name, inn ,address, register_date, register_user_info, phone)
                                       values(@azk_id,@name,@inn,@address,@register_date,@register_user_info,@phone)";

        public const string UpdateAzk = "Update azk set azk_id=@azk_id,name=@name,inn=@inn,address=@address, register_date=@register_date, register_user_info=@register_user_info, phone=@phone  where azk_id=@azk_id";

        public const string DeleteAzk = "Delete from azk where azk_id=@azk_id";
        public const string ReadAzk = "Select * from azk";
        public const string ReadAzkById = "Select * from azk where azk_id=@azk_id";
    }
  

