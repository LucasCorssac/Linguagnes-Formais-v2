using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public enum State { Nulo, Terminais, Variaveis, Inicial, Regras }

    class Gramatica
    {
        public Dictionary<String, List<List<String>>> regrasDeProducao = new Dictionary<string, List<List<string>>>();//Lista de Variaveis que compoem a linguagem
        public List<string> terminais = new List<string>();
        public List<string> variaveis = new List<string>();
        public string inicial;

        //private void readTerminais(System.IO.StreamReader file)
        //{
        //    string line;
        //    while (((line = file.ReadLine()) != null) && !((line = file.ReadLine()).Contains("Terminais"))) ; // le o arquivo até encontrar "terminais"
            
            



        //}

        public Gramatica(System.IO.StreamReader file)
        {      
            //System.IO.StreamReader file = new System.IO.StreamReader(path);

            //Le os Terminais
            
            State state = State.Nulo;
            //bool readTerminais = false;
            //bool readVariaveis = false;
            //bool readInicial = false;
            //bool readRegras = false;
            String pattern = @"\[ ([^\]]+) \]"; // Padrão de pesquisa, procura palavras entre chaves e espaços [ ]
            string line = file.ReadLine();
            do
            {
                //Console.WriteLine("Teste1");

                line = line.Split('#')[0]; // 0 = antes do #, 1 = depois

                //Console.WriteLine(line);

                if (Regex.IsMatch(line, pattern))
                {

                    switch (state)
                    {
                        case State.Terminais:
                            terminais.Add(Regex.Match(line, pattern).Groups[1].Value);
                            break;
                        case State.Variaveis:
                            variaveis.Add(Regex.Match(line, pattern).Groups[1].Value);
                            break;
                        case State.Inicial:
                            inicial = Regex.Match(line, pattern).Groups[1].Value;
                            break;
                        case State.Regras:
                            MatchCollection matches = Regex.Matches(line, pattern);
                            List<String> lstring = new List<string>();
                            for (int i = 1; i < matches.Count; i++)
                                lstring.Add(matches[i].Groups[1].Value);
                            if (regrasDeProducao.ContainsKey(matches[0].Groups[1].Value))
                                regrasDeProducao[matches[0].Groups[1].Value].Add(lstring);
                            else
                            {
                                regrasDeProducao[matches[0].Groups[1].Value] = new List<List<String>>();
                                regrasDeProducao[matches[0].Groups[1].Value].Add(lstring);

                            }

                            //regrasDeProducao.Add(matches[0].Groups[0].Value,
                            //                                                );

                                //foreach (Match match in Regex.Matches(line, pattern))
                                //{
                                //    terminais.Add(match.Groups[1].Value);
                                //}
                            break;

                    }

                }
                else
                {

                    if (line.Contains("Terminais"))
                    {

                        state = State.Terminais;
                    }
                    else if (line.Contains("Variaveis"))
                    {
                        state = State.Variaveis;
                    }
                    else if (line.Contains("Inicial"))
                    {
                        state = State.Inicial;
                    }
                    else if (line.Contains("Regras"))
                    {
                        state = State.Regras;
                    }

                }

            } while ((line = file.ReadLine()) != null);

            file.Close();
            

        }


        public bool isTerminal(string str)
        {
            foreach(string terminal in terminais)
            {
                if(String.Equals(terminal,str))
                {
                    return true;
                }
            }
            return false;
        }

        public bool isVariavel(string str)
        {
            foreach (string variavel in variaveis)
            {
                if (String.Equals(variavel, str))
                {
                    return true;
                }
            }
            return false;

        }


        public void printTerminais()
        {
            Console.WriteLine("Terminais:\n");
            foreach (string terminal in terminais)
            {
                Console.Write("\t");
                Console.WriteLine(terminal);
            }
        }
        public void printVariaveis()
        {
            Console.WriteLine("Variaveis:\n");
            foreach (string variavel in variaveis)
            {
                Console.Write("\t");
                Console.WriteLine(variavel);
            }
        }
        public void printInicial()
        {
            Console.WriteLine("Inicial:\n");
            Console.Write("\t");
            Console.WriteLine(inicial);
        }
        public void printRegras()
        {
            Console.WriteLine("Regras:\n");
            foreach (KeyValuePair<String, List<List<String>>> kvp in regrasDeProducao)
            {   
                foreach (List<String> lstring in kvp.Value)
                {
                    Console.Write("\t");
                    Console.Write("{0} ->", kvp.Key);
                    foreach (String str in lstring)
                    {
                        Console.Write(" {0}", str);
                    }
                    Console.Write("\n");
                }
            }
        }

        public void print()
        {
            printTerminais();
            printVariaveis();
            printInicial();
            printRegras();
        }

    }

   

    


}
