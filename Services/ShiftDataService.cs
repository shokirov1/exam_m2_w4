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
    public class ShiftDataService: IShiftDatas
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

        public bool CreateShiftDatas(ShiftDatas data)
        {
            try
            {
                int res = 0;
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.CommandText = SqlCommands.InsertShiftData;
                        command.Connection = connection;
                        command.Parameters.AddWithValue("shift_id", data.ShiftId);
                        command.Parameters.AddWithValue("azs_id", data.AzsId);
                        command.Parameters.AddWithValue("azk_id", data.AzkId);
                        command.Parameters.AddWithValue("sender_date", data.SenderDate);
                        command.Parameters.AddWithValue("status_code", data.StatusCode);
                        command.Parameters.AddWithValue("xml_report", data.XmlReport);
                        command.Parameters.AddWithValue("error", data.Error);

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

        public List<ShiftDatas> GetShiftDatas()
        {
            try
            {
                List<ShiftDatas> shiftDatas = new();
                using (NpgsqlConnection connection = new(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = SqlCommands.ReadShiftData;

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ShiftDatas shiftdata = new ShiftDatas();
                                shiftdata.ShiftId = reader.GetInt32(0);
                                shiftdata.AzsId = reader.GetInt32(1);
                                shiftdata.AzkId = reader.GetInt32(2);
                                shiftdata.SenderDate = reader.GetDateTime(3);
                                shiftdata.StatusCode = reader.GetInt32(4);
                                shiftdata.XmlReport = reader.GetString(5);
                                shiftdata.Error = reader.GetString(6);
                                
                                shiftDatas.Add(shiftdata);
                            }
                        }
                    }

                    return shiftDatas;
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public ShiftDatas GetShiftDatasById(int shiftid)
        {
            try
            {
                ShiftDatas shiftdata = new();
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = SqlCommands.ReadShiftDataById;
                        command.Parameters.AddWithValue("@shift_id", shiftid);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                shiftdata.ShiftId = reader.GetInt32(0);
                                shiftdata.AzsId = reader.GetInt32(1);
                                shiftdata.AzkId = reader.GetInt32(2);
                                shiftdata.SenderDate = reader.GetDateTime(3);
                                shiftdata.StatusCode = reader.GetInt32(4);
                                shiftdata.XmlReport = reader.GetString(5);
                                shiftdata.Error = reader.GetString(6);
                            }
                            return shiftdata;
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

        public bool UpdateShiftDatas(ShiftDatas data)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = SqlCommands.UpdateShiftData;

                        command.Parameters.AddWithValue("@shift_id", data.ShiftId);
                        command.Parameters.AddWithValue("@azs_id", data.AzsId);
                        command.Parameters.AddWithValue("@azk_id", data.AzkId);
                        command.Parameters.AddWithValue("@sender_date", data.SenderDate);
                        command.Parameters.AddWithValue("@status_code", data.StatusCode);
                        command.Parameters.AddWithValue("@xml_report", data.XmlReport);
                        command.Parameters.AddWithValue("@error", data.Error);
             
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
        public bool DeleteShiftDatas(int shiftid)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(SqlCommands.ConnectionString))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = SqlCommands.DeleteShiftData;
                        cmd.Parameters.AddWithValue("@shift_id", shiftid);
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

        public const string CreateTable = @"CREATE TABLE If not exists shift_data 
                                        (
                                      shift_id INT PRIMARY KEY, 
                                      azs_id INT,
                                      azk_id INT,
                                      sender_date DATE,                                 
                                      status_code INT,
                                      xml_report VARCHAR,
                                      error VARCHAR,
                                      FOREIGN KEY (azs_id) REFERENCES azs (azs_id),
                                      FOREIGN KEY (azk_id) REFERENCES azk (azk_id)
                                        )";


        public const string DropTable = "DROP TABLE if exists shift_data";

        public const string InsertShiftData = @"Insert into shift_data(shift_id, azs_id, azk_id, sender_date ,status_code, xml_report, error)
                                       values(@shift_id, @azs_id, @azk_id, @sender_date, @status_code, @xml_report, @error)";

        public const string UpdateShiftData = "Update shift_data set shift_id=@shift_id, azs_id=@azs_id, azk_id=@azk_id, sender_date=@sender_date, status_code=@status_code, xml_report=@xml_report, error=@error  where shift_id=@shift_id";

        public const string DeleteShiftData = "Delete from shift_data where shift_id=@shift_id";
        public const string ReadShiftData = "Select * from shift_data";
        public const string ReadShiftDataById = "Select * from shift_data where shift_id=@shift_id";
    }
}
