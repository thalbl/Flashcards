﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Models
{
    class Stack
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Flashcard[] Flashcards { get; set; }
    }
}
