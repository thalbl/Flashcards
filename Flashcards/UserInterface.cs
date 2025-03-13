using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flashcards.Models;
using Flashcards;
using System.Data;
using Microsoft.IdentityModel.Tokens;

namespace Flashcards
{
    public class UserInterface {
        internal static void Menu() {
            bool closeApp = false;
            while (closeApp == false) {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\n0 ----- Close application.");
                Console.WriteLine("\n1 ----- Manage Flashcards.");
                Console.WriteLine("\n2 ----- Study.");

                string userInput = Console.ReadLine();
                while (string.IsNullOrEmpty(userInput) || !int.TryParse(userInput, out _)) {
                    Console.WriteLine("Invalid Command. Write a number from 0 to 2.");
                    userInput = Console.ReadLine();
                }

                int command = Convert.ToInt32(userInput);

                switch (command) {
                    case 0:
                        closeApp = true;
                        break;
                    case 1:
                        StacksMenu();
                        break;
                    case 2:
                        StudyMenu();
                        break;
                    default:
                        Console.WriteLine("\nInvalid number, please try again.\n");
                        break;
                }
            }
        }

        internal static void StudyMenu() {
            throw new NotImplementedException();
        }

        internal static void StacksMenu() {
            Console.WriteLine("\n\nFlashcard Menu\n");
            StacksController.GetStacks();
        }

        internal static int GetIntegerInput(string message) {
            Console.WriteLine(message);
            string idInput = Console.ReadLine();

            while(string.IsNullOrEmpty(idInput) || !int.TryParse(idInput, out _)) {
                Console.WriteLine("\nInvalid.");
                idInput = Console.ReadLine();
            }
            return Int32.Parse(idInput);
        }

        internal static string GetStringInput(string message) {
            Console.WriteLine(message);
            string name = Console.ReadLine();
            while (string.IsNullOrEmpty(name)) {
                Console.WriteLine("\nInvalid");
                name = Console.ReadLine();
            }
            return name;
        }

        internal static string GetBinaryInput(string message) {  
            Console.WriteLine(message);
            string option = Console.ReadLine();
            while(string.IsNullOrEmpty(option) && !option.Equals("Y") && !option.Equals("N")) {
                Console.WriteLine("\nInvalid");
                option = Console.ReadLine();
            }
            return option;
        }

        internal static void ManageStackMenu(int id, List<FlashcardWithStack> stack) {
            int stackId = id;

            bool closeArea = false;
            while(closeArea == false) {
                Console.WriteLine("\n\nWhat would you like to do?");
                Console.WriteLine("0 ----- Close Aplication");
                Console.WriteLine("1 ----- Return to Main Menu");
                Console.WriteLine("2 ----- Change stack name");
                Console.WriteLine("3 ----- Delete stack");
                Console.WriteLine("4 ----- Add flashcard");
                Console.WriteLine("5 ----- Delete flashcard");
                Console.WriteLine("6 ----- Update flashcard");

                string commandInput = Console.ReadLine();

                while(string.IsNullOrEmpty(commandInput) || !int.TryParse(commandInput, out _)) {
                    Console.WriteLine("\nInvalid Command. Type a number from 0 to 6\n");
                    commandInput = Console.ReadLine();
                }

                int command = Int32.Parse(commandInput);
                switch (command) {
                    case 0:
                        closeArea = true;
                        break;
                    case 1:
                        Menu();
                        break;
                    case 2:
                        StacksController.UpdateStackName(stackId);
                        break;
                    case 3:
                        StacksController.DeleteStack(stackId);
                        StacksController.GetStacks();
                        break;
                    case 4:
                        FlashcardsController.CreateFlashcard(stackId, null);
                        StacksController.GetStacks();
                        break;
                    case 5:
                        FlashcardsController.DeleteFlashcard(stack);
                        StacksController.GetStacks();
                        break;
                    case 6:
                        FlashcardsController.UpdateFlashcard(stack);
                        StacksController.GetStacks();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
