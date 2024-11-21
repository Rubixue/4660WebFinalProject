using _4660FinalProject.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace _4660FinalProject.Services
{
    public class JSONToMySQLService
    {
        public void InsertSmallJSONDataToSQL()
        {
            // Path to the data.json file
            string jsonFilePath = "../4660WebFinalProject/Data/dataSmall.json";

            jsonFilePath = Path.GetFullPath(jsonFilePath);

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"JSON file not found at path: {jsonFilePath}");
                return;
            }

            // Read and deserialize the JSON
            var jsonData = File.ReadAllText(jsonFilePath);
            var records = JsonConvert.DeserializeObject<List<TemporalRecord>>(jsonData);

            string connectionString = "Server=localhost;Port=3306;Database=TemporalDatabase;User ID=newUser;Password=newPassword;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                foreach (var record in records)
                {
                    // Insert query
                    string query = @"
    INSERT INTO TemporalSmall (id, EMP_NO, Salary, TSTART, END)
    VALUES (@id, @EMP_NO, @Salary, @TSTART, @END)";

                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    // Add parameters
                    cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                    cmd.Parameters.AddWithValue("@EMP_NO", record.EMP_NO);
                    cmd.Parameters.AddWithValue("@Salary", record.Salary);
                    cmd.Parameters.AddWithValue("@TSTART", record.TSTART);
                    cmd.Parameters.AddWithValue("@END", record.END);

                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Data inserted successfully!");
        }

        public void InsertMediumJSONDataToSQL()
        {
            // Path to the data.json file
            string jsonFilePath = "../4660WebFinalProject/Data/dataMedium.json";

            jsonFilePath = Path.GetFullPath(jsonFilePath);

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"JSON file not found at path: {jsonFilePath}");
                return;
            }

            // Read and deserialize the JSON
            var jsonData = File.ReadAllText(jsonFilePath);
            var records = JsonConvert.DeserializeObject<List<TemporalRecord>>(jsonData);

            string connectionString = "Server=localhost;Port=3306;Database=TemporalDatabase;User ID=newUser;Password=newPassword;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                foreach (var record in records)
                {
                    // Insert query
                    string query = @"
    INSERT INTO TemporalMedium (id, EMP_NO, Salary, TSTART, END)
    VALUES (@id, @EMP_NO, @Salary, @TSTART, @END)";

                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    // Add parameters
                    cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                    cmd.Parameters.AddWithValue("@EMP_NO", record.EMP_NO);
                    cmd.Parameters.AddWithValue("@Salary", record.Salary);
                    cmd.Parameters.AddWithValue("@TSTART", record.TSTART);
                    cmd.Parameters.AddWithValue("@END", record.END);

                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Data inserted successfully!");
        }

        public void InsertLargeJSONDataToSQL()
        {
            // Path to the data.json file
            string jsonFilePath = "../4660WebFinalProject/Data/dataLarge.json";

            jsonFilePath = Path.GetFullPath(jsonFilePath);

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"JSON file not found at path: {jsonFilePath}");
                return;
            }

            // Read and deserialize the JSON
            var jsonData = File.ReadAllText(jsonFilePath);
            var records = JsonConvert.DeserializeObject<List<TemporalRecord>>(jsonData);

            string connectionString = "Server=localhost;Port=3306;Database=TemporalDatabase;User ID=newUser;Password=newPassword;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                foreach (var record in records)
                {
                    // Insert query
                    string query = @"
    INSERT INTO TemporalLarge (id, EMP_NO, Salary, TSTART, END)
    VALUES (@id, @EMP_NO, @Salary, @TSTART, @END)";

                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    // Add parameters
                    cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                    cmd.Parameters.AddWithValue("@EMP_NO", record.EMP_NO);
                    cmd.Parameters.AddWithValue("@Salary", record.Salary);
                    cmd.Parameters.AddWithValue("@TSTART", record.TSTART);
                    cmd.Parameters.AddWithValue("@END", record.END);

                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Data inserted successfully!");
        }
    }
}
