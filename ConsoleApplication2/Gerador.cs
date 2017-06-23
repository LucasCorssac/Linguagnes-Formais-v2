﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Essa é minha tentativa de fazer um gerador de frases aleatórias,
 * Ela usa o parser porem ao inves dele parar quando chegou ao final de uma string recebida pelo usuario ela para
 * quando um numero aleatório gerado é par(para ser randomico) e quando o estado que se encontra é um estado
 * se sucesso para o parcer
 * Para isso como char para continuar o parser ele pega um char terminal aleatório que tenha o ponto a sua esquerda no atual estado do algoritimo 
 * e usa ele como parametro para continuar
 */

namespace ConsoleApplication2
{
    class StructState
    {
        public string leftSide;
        public List<string> rightSide;
        public int pointer;
        public int origin;
        
        public StructState(string leftSide, List<string> rightSide, int pointer, int origin)
        {
            this.leftSide = leftSide;
            this.rightSide = rightSide;
            this.pointer = pointer;
            this.origin = origin;   
        }

    }

    class ClassD
    {
        public List<StructState> SList = new List<StructState>();
        public Queue<string> variablesToBeAdded = new Queue<string>();
    }

    class Gerador
    {
        List<ClassD> DList = new List<ClassD>();
        Gramatica gramatica;
        int inputPointer;

        bool success;


        public Gerador(Gramatica gramatica)
        {
            this.gramatica = gramatica;
            this.inputPointer = 0;
            success = false;
        }

        private void predict(ClassD D, StructState stateToPredict)
        {
            string variableToBeAdded = stateToPredict.rightSide[stateToPredict.pointer];

            if (gramatica.isVariavel(variableToBeAdded))
            {
                bool alreadyInAState = false;

                foreach (StructState ss in D.SList)
                {
                    if (string.Equals(ss.leftSide, variableToBeAdded))
                    {
                        alreadyInAState = true;
                    }
                }

                if (!alreadyInAState)
                {
                    foreach (List<string> producao in gramatica.regrasDeProducao[variableToBeAdded])
                    {
                        StructState state = new StructState(variableToBeAdded, producao, 0, inputPointer);
                        D.SList.Add(state);
                        predict(D, state);

                    }

                }

            }
        }

        private void scan(ClassD D, string token)
        {
            if (gramatica.isTerminal(token))
            {
                foreach (StructState state in DList[inputPointer - 1].SList)
                {
                    if(state.pointer < state.rightSide.Count)
                    {
                        if(String.Equals(state.rightSide[state.pointer], token))
                        {
                            StructState nState = new StructState(state.leftSide, state.rightSide, state.pointer + 1, state.origin);
                            D.SList.Add(nState);
                            if(nState.pointer < nState.rightSide.Count)
                            {
                                predict(D, nState);
                            }
                            else
                            {
                                complete(D, nState);
                            }

                        }
                    }
                }
            }

        }

        private void complete(ClassD D, StructState stateToComplete)
        {
            string variableToComplete = stateToComplete.leftSide;

            foreach (StructState state in DList[stateToComplete.origin].SList) 
            {

                if (state.pointer < state.rightSide.Count)
                {
                    if (String.Equals(state.rightSide[state.pointer], variableToComplete))
                    {
                        StructState nState = new StructState(state.leftSide, state.rightSide, state.pointer + 1, state.origin);
                        D.SList.Add(nState);
                        if (nState.pointer < nState.rightSide.Count)
                        {
                            predict(D, nState);
                        }
                        else
                        {
                            complete(D, nState);
                        }
                    }
                }
            }
            
        }

        private void createInitial()
        {
            ClassD D0 = new ClassD();

            StructState D0Initial = new StructState(gramatica.inicial, gramatica.regrasDeProducao[gramatica.inicial][0], 0, 0);

            D0.SList.Add(D0Initial);

            predict(D0, D0Initial);

            DList.Add(D0);

        }

        public void parseGrammar()
        {
            //Aqui é o principal ponto que eu alterei

            createInitial();
            inputPointer = 1;//Ele é usado para saber em qual iteração estamos

            bool Terminou = false;//Bool que vai ser usado para parada aleatória


            string Temp;//String temporaria para comportar terminais

            StringBuilder Builder = new StringBuilder();

            do
            {
                Temp = "";
                success = false;

                ClassD D = new ClassD();

                Random RNG = new Random();

                List<string> TerminaisUteis = PegaTerminais(D, inputPointer);//contem os terminais a direita do ponto no atual estado do parser

                Temp = TerminaisUteis[RNG.Next(TerminaisUteis.Count)];//Guarda o terminal aleatório
                Builder.Append(Temp);//Adciona a lista de todos os terminais aleatórios
                Builder.Append(" ");//Adciona um espaço em nome da beleza

                scan(D, Temp);//Usa como entrada do parser um terminal aleatório dentro da lista de terminais uteis

                DList.Add(D);

                inputPointer++;//Como não é mais um for e sim um do while eu tinha que botar isso em algum lugar

                checkSuccess();//Verifica se o estado atual é um estado de parada para poder entregar essa frase aleatória

                if (success)//Só verifica se pode sair se for um sucesso(levemente mais eficiente)
                    Terminou = (RNG.Next() % 2 == 0 || inputPointer > 300);//Pega um numero aleatório, se ele for par a variavel de saida é verdadeira

            } while (!(success && Terminou));//Verifica se a variavel aleatória permite e saida e se o estado atual nao vai dar merda

            Console.WriteLine(Builder);
        }

        /// <summary>
        /// Função para pegar todos os terminais pertinentes no atual estado
        /// </summary>
        /// <param name="D"></param>
        /// <param name="I"></param>
        /// <returns></returns>
        private List<string> PegaTerminais(ClassD D, int I)
        {

            List<string> Relevantes = new List<string>();

            foreach (StructState State in DList[I - 1].SList)
            {
                if (State.pointer < State.rightSide.Count)
                {
                    if (gramatica.terminais.Contains(State.rightSide[State.pointer]))
                        Relevantes.Add(State.rightSide[State.pointer]);
                }
            }

            return Relevantes;
        }

        private void checkSuccess()
        {
            foreach (StructState state in DList[inputPointer -1].SList)
            {
                if((String.Equals(state.leftSide, gramatica.inicial)) && state.origin == 0 && (state.pointer >= state.rightSide.Count))
                {
                    success = true;
                }
            }

        }

        public bool getSuccess()
        {
            return success;
        }

        public void printAllDs()
        {
            int i = 0; 
            foreach (ClassD d in DList)
            {
                Console.WriteLine("D{0}:", i);

                foreach(StructState ss in d.SList)
                {
                    Console.Write("\t{0}  > ", ss.leftSide);
                    int j = 0;
                    foreach (string s in ss.rightSide)
                    {
                        if (j == ss.pointer)
                            Console.Write(" .");
                        Console.Write(" {0}", s);
                        j++;
                        
                    }
                    if (j == ss.pointer)
                        Console.Write(" .");

                    Console.WriteLine("   /{0}", ss.origin);
                }

                i++;
            }
        }



    }


}