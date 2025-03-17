using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Flashcards.Models;
using Flashcards.Models.DTO;

namespace Flashcards {
    internal class TableVisualizationEngine {
        internal static void PrepareFlashcardList(int id, List<FlashcardWithStack> cards) {
            string stackName = cards.FirstOrDefault().StackName;
            string tableName = $"{id} - {stackName}";

            List<StacksFlashcardsView> stackToView = new List<StacksFlashcardsView>();
            int cardIndex = 1;
            cards.ForEach(x => {
                stackToView.Add(new StacksFlashcardsView {
                    Id = cardIndex,
                    Question = x.Question,
                    Answer = x.Answer
                });
                cardIndex++;
            });
            ShowTable(stackToView, tableName);
        }

        internal static void ShowTable<T>(List<T> stacks, [AllowNull]string tableName) where T : class{
            if(tableName == null) {
                tableName = "";
            }

            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                  .From(stacks)
                  .WithTitle(tableName)
                  .ExportAndWriteLine();
            Console.WriteLine("\n\n");
        }
    }
}