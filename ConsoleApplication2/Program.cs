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
            //ConsoleKey Verificardor;
            string path;//args[0];

            Console.WriteLine("Insira o caminho para o arquivo contendo a linguagem:\n");

            path = "..\\..\\gramatica.txt";//Console.ReadLine();

            Console.WriteLine("\n\n");

            System.IO.StreamReader file = new System.IO.StreamReader(path);

            Gramatica g = new Gramatica(file);

            ConsoleKey Key = new ConsoleKey();

            Parser p = new Parser(g);

            do
            {
                Console.Clear();
                Console.WriteLine("Pressione R para ver as regras da Linguagem.");
                Console.WriteLine("Pressione F para o reconhecedor de frases.");
                Console.WriteLine("Pressione G para entrar no gerador de frases aleatórias.");
                Console.WriteLine("Pressione N para inserir um novo arquivo de linguagem");
                Console.WriteLine("Pressione I para informações sobre o grupo.");
                Console.WriteLine("Pressione Escape para sair.");
                Console.WriteLine("\n");

                Key = Console.ReadKey().Key;

                switch (Key)
                {
                    case ConsoleKey.R:
                        g.print();
                        break;

                    case ConsoleKey.F:
                        Console.WriteLine("Insira uma sequencia de strings a ser parsed, use # para separar strings\nNão use espaços entre as strings e os #:");
                        string input = Console.ReadLine();

                        List<string> ls = new List<string>();
                        string[] inputSplit = input.Split('#');

                        p = new Parser(g);
                        p.printAllDs();
                        break;

                    case ConsoleKey.G:
                        Console.WriteLine("Pressione Enter para gerar novas frases o qualquer tecla para sair.");
                        ConsoleKey ChaveG;
                        do
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine(p.generateSentence(200));
                            Console.WriteLine("\n");

                            ChaveG = Console.ReadKey().Key;

                        } while (ChaveG == ConsoleKey.Enter);
                        break;

                    case ConsoleKey.N:
                        Console.WriteLine("Insira o caminho para o arquivo contendo a linguagem:\n");

                        path = Console.ReadLine();
                        break;

                    case ConsoleKey.I:
                        Console.WriteLine("Grupo (inserir nome do grupo aqui)");
                        Console.WriteLine("Nomes:");
                        Console.WriteLine("Lucas Corssac");
                        Console.WriteLine("Bruno Engracio");
                        Console.WriteLine("Guilherme Silveira");
                        Console.ReadKey();
                        break;

                    case ConsoleKey.Escape:
                        break;
                }
            } while ( Key != ConsoleKey.Escape );


            //Console.WriteLine("Insira uma sequencia de strings a ser parsed, use # para separar strings\nNão use espaços entre as strings e os #:");
            //string input = Console.ReadLine();

            ////List<string> ls = new List<string>();
            //string[] inputSplit = input.Split('#');

            //Parser p = new Parser(g);
            //p.printAllDs();

            //if (p.getSuccess())
            //    Console.WriteLine("\nAceito!!!!!\n\n");
            //else
            //    Console.WriteLine("\n\nNegado!!!!\n\n");

            //Gerador Gera = new Gerador(g);
            //do
            //{
            //    Gera.parseGrammar();

            //    Verificardor = Console.ReadKey().Key;

            //} while (Verificardor == ConsoleKey.Enter);


            //Console.ReadKey();

        }
    }

}
