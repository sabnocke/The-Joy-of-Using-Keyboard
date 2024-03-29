﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ConsoleApp1
{
    public static class ProteinTranslation
    {
        private static string Mapping(string? s)
            => s switch
            {
                "AUG" => "Methionine",
                "UUU" or "UUC" => "Phenylalanine",
                "UUA" or "UUG" => "Leucine",
                "UCU" or "UCC" or "UCA" or "UCG" => "Serine",
                "UAU" or "UAC" => "Tyrosine",
                "UGU" or "UGC" => "Cysteine",
                "UGG" => "Tryptophan",
                "UAA" or "UAG" or "UGA" => "STOP",
                _ => ""
            };

        public static string[] Proteins(string strand)
        {

            var batch = strand.Chunk(3).Select(item => Mapping(new string(item)));
            return new string[10];
        }
    }
    
    internal static class Program
    {
        private static void Main()
        {
            var lst = new string[3];
            lst[0] = "";
            WriteLine(lst.Any(i => i != null));
        }
    }
}
