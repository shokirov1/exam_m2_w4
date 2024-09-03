using exam_m2_w4.Interface;
using exam_m2_w4.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace exam_m2_w4.Services
{
    public class OnlineDataService : IOnlineDatas
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

        public bool CreateOnlineDatas(OnlineDatas onlineDatas)
        {
            try
            {
                int res = 0;
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.CommandText = SqlCommands.InsertOnlineData;
                        command.Connection = connection;
                        command.Parameters.AddWithValue("online_data_id", onlineDatas.OnlineDataId);
                        command.Parameters.AddWithValue("shift_id", onlineDatas.ShiftId);
                        command.Parameters.AddWithValue("azs_id", onlineDatas.AzsId);
                        command.Parameters.AddWithValue("azk_id", onlineDatas.AzkId);
                        command.Parameters.AddWithValue("sender_date", onlineDatas.SenderDate);
                        command.Parameters.AddWithValue("status_code", onlineDatas.StatusCode);
                        command.Parameters.AddWithValue("xml_report", onlineDatas.XmlReport);
                        command.Parameters.AddWithValue("error", onlineDatas.Error);

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

        public List<OnlineDatas> GetOnlineDatas()
        {
            try
            {
                List<OnlineDatas> onlineDatas = new();
                using (NpgsqlConnection connection = new(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = SqlCommands.ReadOnlineData;

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OnlineDatas onlinedata = new OnlineDatas();
                                onlinedata.OnlineDataId = reader.GetInt32(0);
                                onlinedata.ShiftId = reader.GetInt32(1);
                                onlinedata.AzsId = reader.GetInt32(2);
                                onlinedata.AzkId = reader.GetInt32(3);
                                onlinedata.SenderDate = reader.GetDateTime(4);
                                onlinedata.StatusCode = reader.GetInt32(5);
                                onlinedata.XmlReport = reader.GetString(6);
                                onlinedata.Error = reader.GetString(7);

                                onlineDatas.Add(onlinedata);
                            }
                        }
                    }

                    return onlineDatas;
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public OnlineDatas GetOnlineDataById(int id)
        {
            try
            {
                OnlineDatas onlinedata = new();
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = SqlCommands.ReadShiftOnlineById;
                        command.Parameters.AddWithValue("@online_data_id", id);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                onlinedata.ShiftId = reader.GetInt32(0);
                                onlinedata.OnlineDataId = reader.GetInt32(0);
                                onlinedata.ShiftId = reader.GetInt32(1);
                                onlinedata.AzsId = reader.GetInt32(2);
                                onlinedata.AzkId = reader.GetInt32(3);
                                onlinedata.SenderDate = reader.GetDateTime(4);
                                onlinedata.StatusCode = reader.GetInt32(5);
                                onlinedata.XmlReport = reader.GetString(6);
                                onlinedata.Error = reader.GetString(7);
                            }
                            return onlinedata;
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

        public bool UpdateOnlineDatas(OnlineDatas onlineDatas)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = SqlCommands.UpdateOnlineData;

                        command.Parameters.AddWithValue("@online_data_id", onlineDatas.OnlineDataId);
                        command.Parameters.AddWithValue("@shift_id", onlineDatas.ShiftId);
                        command.Parameters.AddWithValue("@azs_id", onlineDatas.AzsId);
                        command.Parameters.AddWithValue("@azk_id", onlineDatas.AzkId);
                        command.Parameters.AddWithValue("@sender_date", onlineDatas.SenderDate);
                        command.Parameters.AddWithValue("@status_code", onlineDatas.StatusCode);
                        command.Parameters.AddWithValue("@xml_report", onlineDatas.XmlReport);
                        command.Parameters.AddWithValue("@error", onlineDatas.Error);

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
        public bool DeleteOnlineDatas(int id)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = SqlCommands.DeleteOnlineData;
                        cmd.Parameters.AddWithValue("@online_data_id", id);
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

        public const string CreateTable = @"CREATE TABLE If not exists online_data 
                                        (

                                      online_data_id INT PRIMARY KEY, 
                                      shift_id INT, 
                                      azs_id INT,
                                      azk_id INT,
                                      sender_date DATE,                                 
                                      status_code INT,
                                      xml_report VARCHAR,
                                      error VARCHAR,
                                      FOREIGN KEY (shift_id) REFERENCES shift_data (shift_id),
                                      FOREIGN KEY (azs_id) REFERENCES azs (azs_id),
                                      FOREIGN KEY (azk_id) REFERENCES azk (azk_id)
                                        )";


        public const string DropTable = "DROP TABLE if exists online_data";

        public const string InsertOnlineData = @"Insert into online_data(online_data_id, shift_id, azs_id, azk_id, sender_date ,status_code, xml_report, error)
                                       values(@online_data_id, @shift_id, @azs_id, @azk_id, @sender_date, @status_code, @xml_report, @error)";

        public const string UpdateOnlineData = "Update online_data set online_data_id=@online_data_id, shift_id=@shift_id, azs_id=@azs_id, azk_id=@azk_id, sender_date=@sender_date, status_code=@status_code, xml_report=@xml_report, error=@error  where online_data_id=@online_data_id";

        public const string DeleteOnlineData = "Delete from online_data where online_data_id=@online_data_id";
        public const string ReadOnlineData = "Select * from online_data";
        public const string ReadShiftOnlineById = "Select * from online_data where online_data_id=@online_data_id";
    }
}
