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

        private static List<FlashcardWithStack> GetStackWithCards(int stackId) {
            throw new NotImplementedException();
        }

        internal static void GetStacks() {
            throw new NotImplementedException();
        }
    }
}
