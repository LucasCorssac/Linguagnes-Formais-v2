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
        struct blabla {
            int blabla_a;
        }

        static void fileTester(System.IO.StreamReader file)
        {
            Console.WriteLine(file.ReadLine());
        }

        static void Main()
        {
            //System.IO.StreamReader file = new System.IO.StreamReader("..\\..\\gramatica.txt");

            //String line;

            //line = file.ReadLine();
            //String linesplit = line.Split('#')[0];

            //Console.WriteLine(linesplit.Contains("Terminais"));
            //Console.WriteLine(linesplit);

            //Console.WriteLine("Chamada:");
            //fileTester(file);
            //Console.WriteLine("Depois da chamada:");
            //Console.WriteLine(file.ReadLine());

            //List<String> lstring = new List<string>();

            //lstring.Add("abc");

            //Console.WriteLine(lstring[0]);

            //List<blabla> lblabla = new List<blabla>();

            //String inputString = "2 + 3 * 4";

            //String[] terminalArray = inputString.Split(' ');

            ////Gramatica gramatica = new Gramatica();

            ////Console.WriteLine(terminalArray.Length);

            //for (var i = 0; i <= terminalArray.Length; i++)
            //{
            //    Console.WriteLine(i);
            //}

            //Dictionary<String, String> mydict = new Dictionary<string, string>();

            //mydict.Add("lucas", "Corssac");

            //Console.WriteLine(mydict["lucas"]);

            //mydict["Laura"] = "ass";

            //Console.WriteLine(mydict["Laura"]);

            ////Console.WriteLine(mydict["Johnny"]);



            //string test = "[ lucas bragante corssac ] > [ amigo ]";
            //String pattern = @"\[ ([^\]]+) \]";

            //Console.WriteLine("\nRegex:");

            //Console.WriteLine(Regex.IsMatch(test,pattern));


            //foreach (Match match in Regex.Matches(test, pattern))
            //{
            //    Console.WriteLine(match);
            //}

            //file.Close();

            //file = new System.IO.StreamReader("..\\..\\gramatica.txt");

            //Console.WriteLine("\nTESE DA GRAMATICA\n");
            //Gramatica g1 = new Gramatica(file);

            //g1.printTerminais();
            //g1.printVariaveis();
            //g1.printInicial();
            //g1.printRegras();

            //file.Close();

            //file = new System.IO.StreamReader("..\\..\\gramatica 2.txt");

            //Console.WriteLine("\nTESE DA GRAMATICA\n");
            //Gramatica g2 = new Gramatica(file);

            //g2.printTerminais();
            //g2.printVariaveis();
            //g2.printInicial();
            //g2.printRegras();

            //Console.WriteLine (g2.regrasDeProducao["M"][0][1]);


            //Console.WriteLine(g2.isTerminal("1"));
            //Console.WriteLine(g2.isVariavel("1"));
            //Console.WriteLine(g2.isVariavel("M"));

            //int a = 2;
            //Console.WriteLine(a = 3);

            //List<string> abc = new List<string>();
            //Console.WriteLine(abc[3]); 

            //string outtest = Regex.Match(test, pattern).Value;

            //Console.WriteLine("\n{0}", outtest);

            //List<String> abc = new List<string>();
            //abc.Add("a");
            //abc.Add("c");

            //int j = 0;
            //foreach (string x in abc)
            //{
            //    if (j%2 == 0)
            //    {
            //        abc.Add("Lucas");
            //    }

            //    if (j == 10)
            //    {
            //        break;
            //    }
            //    j++;
            //}

            //foreach (string x in abc)
            //{
            //    Console.WriteLine(x);
            //}
            //int i = 1;
            //foreach (var x in linesplit)
            //{
            //    //Console.WriteLine(i);
            //    Console.WriteLine("{0} : {1}", i, x); //tst
            //    Console.WriteLine(x.Contains("Terminais"));
            //    i++;
            //}

            // Console.WriteLine(line.Contains("Terminais"));

            System.IO.StreamReader file = new System.IO.StreamReader("..\\..\\gramatica.txt");

            Gramatica g = new Gramatica(file);

            Parser p = new Parser(g, "blablabla");

            p.parseGrammar();
            p.printAllDs();

        }
    }

}
