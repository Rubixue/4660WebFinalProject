using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using _4660FinalProject.Models;

namespace _4660FinalProject.Services
{
    public class TemporalCoalescingService
    {
        private string connectionString = "Server=localhost;Port=3306;Database=TemporalDatabase;User ID=newUser;Password=newPassword;";

        // Pure SQL Coalescing Query
        public List<TemporalRecord> CoalesceUsingSQLSmall()
        {
            var result = new List<TemporalRecord>();

            string query = @"
            WITH Temp (EMP_NO, Salary, TSTART, TEND) AS (
                SELECT EMP_NO, Salary, TSTART, END
                FROM TemporalSmall
            )
            SELECT DISTINCT 
                F.EMP_NO, 
                F.Salary, 
                F.TSTART, 
                F.TEND
            FROM Temp AS F
            WHERE NOT EXISTS (
                SELECT *
                FROM Temp AS L
                WHERE L.EMP_NO = F.EMP_NO -- Ensure EMP_NO matches
                  AND L.Salary = F.Salary
                  AND L.TSTART < F.TSTART
                  AND F.TSTART <= L.TEND
            )
            AND NOT EXISTS (
                SELECT *
                FROM Temp AS R
                WHERE R.EMP_NO = F.EMP_NO -- Ensure EMP_NO matches
                  AND R.Salary = F.Salary
                  AND F.TEND >= R.TSTART
                  AND R.TSTART > F.TSTART
            );";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new TemporalRecord
                            {
                                EMP_NO = reader.GetInt32("EMP_NO"),
                                Salary = reader.GetDecimal("Salary"),
                                TSTART = reader.GetDateTime("TSTART"),
                                END = reader.GetDateTime("TEND")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during SQL coalescing: " + ex.ToString());
            }

            return result.OrderBy(r => r.Salary).ThenBy(r => r.TSTART).ToList();
        }

        public List<TemporalRecord> CoalesceUsingSQLMedium()
        {
            var result = new List<TemporalRecord>();

            string query = @"
            WITH Temp (EMP_NO, Salary, TSTART, TEND) AS (
                SELECT EMP_NO, Salary, TSTART, END
                FROM TemporalMedium
            )
            SELECT DISTINCT 
                F.EMP_NO, 
                F.Salary, 
                F.TSTART, 
                F.TEND
            FROM Temp AS F
            WHERE NOT EXISTS (
                SELECT *
                FROM Temp AS L
                WHERE L.EMP_NO = F.EMP_NO -- Ensure EMP_NO matches
                  AND L.Salary = F.Salary
                  AND L.TSTART < F.TSTART
                  AND F.TSTART <= L.TEND
            )
            AND NOT EXISTS (
                SELECT *
                FROM Temp AS R
                WHERE R.EMP_NO = F.EMP_NO -- Ensure EMP_NO matches
                  AND R.Salary = F.Salary
                  AND F.TEND >= R.TSTART
                  AND R.TSTART > F.TSTART
            );";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new TemporalRecord
                            {
                                EMP_NO = reader.GetInt32("EMP_NO"),
                                Salary = reader.GetDecimal("Salary"),
                                TSTART = reader.GetDateTime("TSTART"),
                                END = reader.GetDateTime("TEND")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during SQL coalescing: " + ex.ToString());
            }

            return result.OrderBy(r => r.Salary).ThenBy(r => r.TSTART).ToList();
        }

        public List<TemporalRecord> CoalesceUsingSQLLarge()
        {
            var result = new List<TemporalRecord>();

            string query = @"
            WITH Temp (EMP_NO, Salary, TSTART, TEND) AS (
                SELECT EMP_NO, Salary, TSTART, END
                FROM TemporalLarge
            )
            SELECT DISTINCT 
                F.EMP_NO, 
                F.Salary, 
                F.TSTART, 
                F.TEND
            FROM Temp AS F
            WHERE NOT EXISTS (
                SELECT *
                FROM Temp AS L
                WHERE L.EMP_NO = F.EMP_NO -- Ensure EMP_NO matches
                  AND L.Salary = F.Salary
                  AND L.TSTART < F.TSTART
                  AND F.TSTART <= L.TEND
            )
            AND NOT EXISTS (
                SELECT *
                FROM Temp AS R
                WHERE R.EMP_NO = F.EMP_NO -- Ensure EMP_NO matches
                  AND R.Salary = F.Salary
                  AND F.TEND >= R.TSTART
                  AND R.TSTART > F.TSTART
            );";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new TemporalRecord
                            {
                                EMP_NO = reader.GetInt32("EMP_NO"),
                                Salary = reader.GetDecimal("Salary"),
                                TSTART = reader.GetDateTime("TSTART"),
                                END = reader.GetDateTime("TEND")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during SQL coalescing: " + ex.ToString());
            }

            return result.OrderBy(r => r.Salary).ThenBy(r => r.TSTART).ToList();
        }




        // Single-Scan Coalescing Algorithm
        public List<TemporalRecord> CoalesceUsingSingleScanSmall()
        {
            var records = new List<TemporalRecord>();

            // Step 1: Fetch all data from the new database table
            string query = "SELECT EMP_NO, Salary, TSTART, END FROM TemporalSmall";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        records.Add(new TemporalRecord
                        {
                            EMP_NO = reader.GetInt32("EMP_NO"),
                            Salary = reader.GetDecimal("Salary"),
                            TSTART = reader.GetDateTime("TSTART"),
                            END = reader.GetDateTime("END")
                        });
                    }
                }
            }

            // Step 2: Group by Salary and sort each group by TSTART
            var result = new List<TemporalRecord>();
            var groupedRecords = records
                .GroupBy(r => r.Salary)
                .Select(group => group.OrderBy(r => r.TSTART).ToList())
                .ToList();

            // Step 3: Apply single-scan coalescing for each group
            foreach (var group in groupedRecords)
            {
                TemporalRecord temp = group[0]; // Initialize with the first tuple

                for (int i = 1; i < group.Count; i++)
                {
                    if (group[i].TSTART <= temp.END) // Coalesce if overlapping
                    {
                        temp.END = temp.END > group[i].END ? temp.END : group[i].END;
                    }
                    else
                    {
                        result.Add(temp); // Add the current coalesced period
                        temp = group[i]; // Start a new interval
                    }
                }

                result.Add(temp); // Add the last interval of the group
            }

            return result;
        }

        public List<TemporalRecord> CoalesceUsingSingleScanMedium()
        {
            var records = new List<TemporalRecord>();

            // Step 1: Fetch all data from the new database table
            string query = "SELECT EMP_NO, Salary, TSTART, END FROM TemporalMedium";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        records.Add(new TemporalRecord
                        {
                            EMP_NO = reader.GetInt32("EMP_NO"),
                            Salary = reader.GetDecimal("Salary"),
                            TSTART = reader.GetDateTime("TSTART"),
                            END = reader.GetDateTime("END")
                        });
                    }
                }
            }

            // Step 2: Group by Salary and sort each group by TSTART
            var result = new List<TemporalRecord>();
            var groupedRecords = records
                .GroupBy(r => r.Salary)
                .Select(group => group.OrderBy(r => r.TSTART).ToList())
                .ToList();

            // Step 3: Apply single-scan coalescing for each group
            foreach (var group in groupedRecords)
            {
                TemporalRecord temp = group[0]; // Initialize with the first tuple

                for (int i = 1; i < group.Count; i++)
                {
                    if (group[i].TSTART <= temp.END) // Coalesce if overlapping
                    {
                        temp.END = temp.END > group[i].END ? temp.END : group[i].END;
                    }
                    else
                    {
                        result.Add(temp); // Add the current coalesced period
                        temp = group[i]; // Start a new interval
                    }
                }

                result.Add(temp); // Add the last interval of the group
            }

            return result;
        }

        public List<TemporalRecord> CoalesceUsingSingleScanLarge()
        {
            var records = new List<TemporalRecord>();

            // Step 1: Fetch all data from the new database table
            string query = "SELECT EMP_NO, Salary, TSTART, END FROM TemporalLarge";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        records.Add(new TemporalRecord
                        {
                            EMP_NO = reader.GetInt32("EMP_NO"),
                            Salary = reader.GetDecimal("Salary"),
                            TSTART = reader.GetDateTime("TSTART"),
                            END = reader.GetDateTime("END")
                        });
                    }
                }
            }

            // Step 2: Group by Salary and sort each group by TSTART
            var result = new List<TemporalRecord>();
            var groupedRecords = records
                .GroupBy(r => r.Salary)
                .Select(group => group.OrderBy(r => r.TSTART).ToList())
                .ToList();

            // Step 3: Apply single-scan coalescing for each group
            foreach (var group in groupedRecords)
            {
                TemporalRecord temp = group[0]; // Initialize with the first tuple

                for (int i = 1; i < group.Count; i++)
                {
                    if (group[i].TSTART <= temp.END) // Coalesce if overlapping
                    {
                        temp.END = temp.END > group[i].END ? temp.END : group[i].END;
                    }
                    else
                    {
                        result.Add(temp); // Add the current coalesced period
                        temp = group[i]; // Start a new interval
                    }
                }

                result.Add(temp); // Add the last interval of the group
            }

            return result;
        }



    }
}
