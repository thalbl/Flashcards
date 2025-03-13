using System;
using Microsoft.Data.SqlClient;

namespace Flashcards {
    class Program {
        static void Main(string[] args) {
            DatabaseManager.CheckDatabase();
        }
    }
}

