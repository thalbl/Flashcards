using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Flashcards.Models;
using Flashcards.Models.DTO;
using ConsoleTableExt;


namespace Flashcards {
    class StacksController {
        public static readonly string connectionString = "Server=(localdb)\\Local;Integrated Security=true; Database=quizDb;";

        public static void ManageStack() {
            GetStacks();
            int stackId = UserInterface.GetIntegerInput("\nType the stack's id from wich you'd like to manage\n");
            List<FlashcardWithStack> stack = GetStackWithCards(stackId);
            UserInterface.ManageStackMenu(stackId, stack);
        }


        internal static List<Stack> GetStacks() {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $@"SELECT * FROM stack";

            List<Stack> stacks = new();

            SqlDataReader reader = tableCmd.ExecuteReader();

            if (reader.HasRows) {
                while (reader.Read()) {
                    stacks.Add(
                        new Stack {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                }
            }
            else {
                Console.WriteLine("No rows found.");
            }

            string[] columns = { "Id", "Name" };
            TableVisualizationEngine.ShowTable(stacks, null);
            return stacks;
        }

        private static List<FlashcardWithStack> GetStackWithCards(int id) {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $@"
                SELECT f.Id, s.Name as stackname, f.Question, f.Answer
                    FROM flashcard f
                    LEFT JOIN stack s
                    ON s.Id = f.StackId
                    WHERE s.Id = {id}";

            List<FlashcardWithStack> cards = new();
            SqlDataReader reader = tableCmd.ExecuteReader();
            if (reader.HasRows) {
                while (reader.Read()) {
                    cards.Add(
                        new FlashcardWithStack {
                            Id = reader.GetInt32(0),
                            StackName = reader.GetString(1),
                            Question = reader.GetString(2),
                            Answer = reader.GetString(3)
                        });
                }
            }
            else {
                Console.WriteLine("\nNo rows found.");
            }

            reader.Close();
            Console.WriteLine("\n\n");
            TableVisualizationEngine.PrepareFlashcardList(id, cards);
            return cards;
        }

        internal static void CreateStack() {
            Stack stack = new();
            Console.WriteLine("\n\nEnter stack name: ");
            stack.Name = Console.ReadLine();

            SqlConnection connection = new(connectionString);

            using (connection) {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $@"
                    INSERT INTO stack (Name) VALUES ('{stack.Name}')";
                tableCmd.ExecuteNonQuery();
                connection.Close();
            }

            var stackId = GetStackId();
            Console.WriteLine("\n\nYour flashcard was created!\n");
            FlashcardsController.CreateFlashcard(stackId, stack.Name);
        }
    }
}
