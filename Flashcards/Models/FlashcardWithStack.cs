﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Models
{
    class FlashcardWithStack
    {
        public int Id { get; set; }
        public string StackName { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
