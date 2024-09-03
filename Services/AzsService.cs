using exam_m2_w4.Interface;
using exam_m2_w4.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace exam_m2_w4.Services
{
    public class AzsService:IAzs
    {
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

        public bool CreateAzs(Azs azs)
        {
            try
            {
                int res = 0;
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.CommandText = SqlCommands.InsertAzs;
                        command.Connection = connection;
                        command.Parameters.AddWithValue("azs_id", azs.AzsId);
                        command.Parameters.AddWithValue("azk_id", azs.AzkId);
                        command.Parameters.AddWithValue("name", azs.Name);
                        command.Parameters.AddWithValue("inn", azs.Inn);
                        command.Parameters.AddWithValue("address", azs.Address);
                        command.Parameters.AddWithValue("register_date", azs.RegisterDate);
                        command.Parameters.AddWithValue("register_user_info", azs.RegisterUserInfo);
                        command.Parameters.AddWithValue("phone", azs.Phone);
                        command.Parameters.AddWithValue("hasp_key", azs.HaspKey);
                        command.Parameters.AddWithValue("last_connection_date", azs.LastConnectionDate);
                        command.Parameters.AddWithValue("last_online_transaction_date", azs.LastOnlineTransactionDate);
                        command.Parameters.AddWithValue("is_archived", azs.IsArchived);

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

        public List<Azs> GetAzs()
        {
            try
            {
                List<Azs> azss = new();
                using (NpgsqlConnection connection = new(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = SqlCommands.ReadAzs;

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Azs azs = new Azs();
                                azs.AzsId = reader.GetInt32(0);
                                azs.AzkId = reader.GetInt32(1);
                                azs.Name = reader.GetString(2);
                                azs.Address = reader.GetString(3);
                                azs.Inn = reader.GetString(4);
                                azs.RegisterDate = reader.GetDateTime(5);
                                azs.RegisterUserInfo = reader.GetString(6);
                                azs.Phone = reader.GetString(7);
                                azs.HaspKey = reader.GetInt32(8);
                                azs.LastConnectionDate = reader.GetDateTime(9);
                                azs.LastOnlineTransactionDate = reader.GetDateTime(10);
                                azs.IsArchived = reader.GetBoolean(11);
                                azss.Add(azs);
                            }
                        }
                    }

                    return azss;
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Azs GetAzsById(int azsid)
        {
            try
            {
                Azs azs = new();
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = SqlCommands.ReadAzsById;
                        command.Parameters.AddWithValue("@azk_id", azsid);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                azs.AzsId = reader.GetInt32(0);
                                azs.AzkId = reader.GetInt32(1);
                                azs.Name = reader.GetString(2);
                                azs.Address = reader.GetString(3);
                                azs.Inn = reader.GetString(4);
                                azs.RegisterDate = reader.GetDateTime(5);
                                azs.RegisterUserInfo = reader.GetString(6);
                                azs.Phone = reader.GetString(7);
                                azs.HaspKey = reader.GetInt32(8);
                                azs.LastConnectionDate = reader.GetDateTime(9);
                                azs.LastOnlineTransactionDate = reader.GetDateTime(10);
                                azs.IsArchived = reader.GetBoolean(11);
                            }
                            return azs;
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

        public bool UpdateAzs(Azs azs)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = SqlCommands.UpdateAzs;

                        command.Parameters.AddWithValue("@azs_id", azs.AzsId);
                        command.Parameters.AddWithValue("@azk_id", azs.AzkId);
                        command.Parameters.AddWithValue("@name", azs.Name);
                        command.Parameters.AddWithValue("@inn", azs.Inn);
                        command.Parameters.AddWithValue("@address", azs.Address);
                        command.Parameters.AddWithValue("@register_date", azs.RegisterDate);
                        command.Parameters.AddWithValue("@register_user_info", azs.RegisterUserInfo);
                        command.Parameters.AddWithValue("@phone", azs.Phone);
                        command.Parameters.AddWithValue("@hasp_key", azs.HaspKey);
                        command.Parameters.AddWithValue("@last_connection_date", azs.LastConnectionDate);
                        command.Parameters.AddWithValue("@last_online_transaction_date", azs.LastOnlineTransactionDate);
                        command.Parameters.AddWithValue("@is_archived", azs.IsArchived);

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

        public bool DeleteAzs(int azsid)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = SqlCommands.DeleteAzs;
                        cmd.Parameters.AddWithValue("@azs_id", azsid);
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
    file class SqlCommands
    {

        public const string ConnectionString = "Server=localhost;Port=5432;Database=monitoring_db;Username=postgres;Password=1939;";

        public const string CreateTable = @"CREATE TABLE If not exists azs 
                                        (
                                      azs_id INT PRIMARY KEY, 
                                      azk_id INT,
                                      name VARCHAR(50) NOT NULL,
                                      inn VARCHAR(50) NOT NULL,
                                      address VARCHAR(100) NOT NULL,
                                      register_date DATE,
                                      register_user_info VARCHAR(100),
                                      phone VARCHAR(100),
                                      hasp_key INT UNIQUE,
                                      last_connection_date DATE,
                                      last_online_transaction_date DATE,
                                      is_archived boolean,
                                      FOREIGN KEY (azk_id) REFERENCES azk (azk_id)
                                        )";

        public const string DropTable = "DROP TABLE if exists azs";

        public const string InsertAzs = @"Insert into azs(azs_id, azk_id, name, inn ,address, register_date, register_user_info, phone, hasp_key, last_connection_date, last_online_transaction_date, is_archived)
                                       values(@azs_id,@azk_id,@name,@inn,@address,@register_date,@register_user_info,@phone,@hasp_key,@last_connection_date,@last_online_transaction_date,@is_archived)";

        public const string UpdateAzs = "Update azs set azs_id=@azs_id,azk_id=@azk_id,name=@name,inn=@inn,address=@address, register_date=@register_date, register_user_info=@register_user_info, phone=@phone, hasp_key=@hasp_key, last_connection_date=@last_connection_date, last_online_transaction_date=@last_online_transaction_date, is_archived=@is_archived  where azs_id=@azs_id";

        public const string DeleteAzs = "Delete from azs where azs_id=@azs_id";
        public const string ReadAzs = "Select * from azs";
        public const string ReadAzsById = "Select * from azs where azs_id=@azs_id";
    }
}
