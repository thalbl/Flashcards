using System;
using Microsoft.Data.SqlClient;

namespace Flashcards {
    internal class DatabaseManager {
        private static string connectionString = "Server=(localdb)\\Local;Integrated Security=true;";

        internal static void CheckDatabase() {
            try {
                // Use the initial connection string (without database) to create the database
                string initialConnectionString = connectionString;
                using (SqlConnection connection = new SqlConnection(initialConnectionString)) {
                    connection.Open();
                    var tableCmd = connection.CreateCommand();
                    tableCmd.CommandText = @"
                        IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'quizDb')
                        BEGIN
                            CREATE DATABASE quizDb;
                        END;";
                    tableCmd.ExecuteNonQuery();
                    connection.Close();
                }

                // Update the connection string to include the database
                connectionString += "Database=quizDb;";

                CreateTables();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        private static void CreateTables() {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                // Create 'stack' table if it doesn't exist
                tableCmd.CommandText = @"
                    IF NOT EXISTS (
                        SELECT * 
                        FROM sys.tables 
                        WHERE name = 'stack' AND schema_id = SCHEMA_ID('dbo')
                    )
                    CREATE TABLE stack (
                        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                        Name VARCHAR(100) NOT NULL UNIQUE
                    );";
                tableCmd.ExecuteNonQuery();

                // Create 'flashcard' table if it doesn't exist
                tableCmd.CommandText = @"
                    IF NOT EXISTS (
                        SELECT * 
                        FROM sys.tables 
                        WHERE name = 'flashcard' AND schema_id = SCHEMA_ID('dbo')
                    )
                    CREATE TABLE flashcard (
                        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                        Question VARCHAR(30) NOT NULL,
                        Answer VARCHAR(30) NOT NULL,
                        StackId INT NOT NULL,
                        FOREIGN KEY (StackId) REFERENCES stack(Id)
                            ON DELETE CASCADE
                            ON UPDATE CASCADE
                    );";
                tableCmd.ExecuteNonQuery();

                Console.WriteLine("Tables created successfully");
                connection.Close();
            }
        }
    }
}