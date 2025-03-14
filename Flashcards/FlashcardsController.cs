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
    internal class FlashcardsController {
        public static readonly string connectionString = "Server=(localdb)\\Local;Integrated Security=true; Database=quizDb;";

        internal static void CreateFlashcard(int stackId, string name) {
            Console.WriteLine($"Creating flashcard! \nStack id: {stackId}\nFlashcard name: {name}");
            bool createFlashcard = true;
            while (createFlashcard) {
                Flashcard flashcard = new();

                Console.WriteLine("\nEnter with the question:\t");
                string question = Console.ReadLine();

                SqlConnection connection = new(connectionString);
                using (connection) {
                    connection.Open();
                    var tableCmd = connection.CreateCommand();
                    tableCmd.CommandText = $@"
                            INSERT INTO flashcard (Question, Answer, StackId)
                            VALUES ('{flashcard.Question}', '{flashcard.Answer}', '{stackId}'";
                    tableCmd.ExecuteNonQuery();

                    Console.WriteLine("Would like to add another flashcard?");
                    string anotherFlashcard = Console.ReadLine();

                    while(anotherFlashcard.ToUpper() != "Y" && anotherFlashcard.ToUpper() != "N") {
                        Console.WriteLine("Invalid input, please use Y/N");
                        anotherFlashcard = Console.ReadLine().ToUpper();

                        if(anotherFlashcard.ToUpper() == "Y" || anotherFlashcard.ToUpper() == "N") {
                            return;
                        }
                    }

                    if (anotherFlashcard.ToUpper() == "N") {
                        createFlashcard = false;
                    }
               
                }
            }
            
        }

        internal static void DeleteFlashcard(List<FlashcardWithStack> flashcardsList) {
            int flashcardIdOnView = UserInterface.GetIntegerInput("\nWhich flashcard do you want to delete? ");
            int flashcardId = flashcardsList.Select(x => x.Id).ElementAt(flashcardIdOnView - 1);

            SqlConnection connection = new(connectionString);

            using (connection) {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $@"
                        DELETE FROM flashcard WHERE Id = {flashcardId}";
                tableCmd.ExecuteNonQuery();
                connection.Close();
            }

            Console.WriteLine("Flashcard deleted.");
        }

        internal static void UpdateFlashcard(List<FlashcardWithStack> flashcardsList) {
            int flashcardIdOnView = UserInterface.GetIntegerInput("\nWhich flashcard do you want to update? ");
            int flashcardId = flashcardsList.Select(x => x.Id).ElementAt(flashcardIdOnView - 1);

            string updateCommand = "";
            string newQuestion = "";
            string newAnswer = "";

            string questionOption = UserInterface.GetBinaryInput("\nWant to update the flashcard Question?");
            if(questionOption == "Y") {
                newQuestion = UserInterface.GetStringInput("\nWrite new question: ");
            }

            questionOption = UserInterface.GetBinaryInput("\nWant to update the flashcard answer?");
            if(questionOption == "Y") {
                newAnswer = UserInterface.GetStringInput("\nWrite new answer");
            }

            if(newQuestion == "") {
                updateCommand = $@"UPDATE flashcard SET Answer = '{newAnswer}' WHERE Id = {flashcardId}";
            }
            else if(newAnswer == "") {
                updateCommand = $@"UPDATE flashcard SET Question = '{newQuestion}' WHERE Id = {flashcardId}";
            }
            else {
                updateCommand = $@"UPDATE flashcard SET Answer = '{newAnswer}', Question = '{newQuestion}' WHERE Id = {flashcardId}";
            }

            SqlConnection connection = new(connectionString);
            using (connection) {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = updateCommand;
                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
            Console.WriteLine("\n\nFlashcard successfully updated");
        }
    }
}