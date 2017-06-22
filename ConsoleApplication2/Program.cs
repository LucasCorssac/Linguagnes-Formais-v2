using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\Guilherme\\Desktop\\gramatica.txt";//args[0];
            System.IO.StreamReader file = new System.IO.StreamReader(path);

            Gramatica g = new Gramatica(file);

            Console.WriteLine("Insira uma sequencia de strings a ser parsed, use # para separar strings\nNão use espaços entre as strings e os #:");
            string input = Console.ReadLine();

            //List<string> ls = new List<string>();
            string[] inputSplit = input.Split('#');

            Parser p = new Parser(g, inputSplit);
            p.printAllDs();

            if (p.getSuccess())
                Console.WriteLine("\n\nSuccess!!!!!");

        }
    }

}
