using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class StateStruct
    {
        public string leftSide;
        public List<string> rightSide;
        public int pointer;
        public int origin;
        
        public StateStruct(string leftSide, List<string> rightSide, int pointer, int origin)
        {
            this.leftSide = leftSide;
            this.rightSide = rightSide;
            this.pointer = pointer;
            this.origin = origin;   
        }

    }

    class DClass
    {
        public List<StateStruct> SList = new List<StateStruct>();
        public Queue<string> variablesToBeAdded = new Queue<string>();
    }

    class Parser
    {
        List<DClass> DList = new List<DClass>();
        Gramatica gramatica;
        string[] inputStringArray;
        int inputPointer;

        public Parser(Gramatica gramatica, string[] inputStringArray)
        {
            this.gramatica = gramatica;
            this.inputStringArray = inputStringArray;
            this.inputPointer = 0;
        }


        private void predict(DClass D, StateStruct stateToPredict)
        {
            string variableToBeAdded = stateToPredict.rightSide[stateToPredict.pointer];

            if (gramatica.isVariavel(variableToBeAdded))
            {
                bool alreadyInAState = false;

                foreach (StateStruct ss in D.SList)
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
                        StateStruct state = new StateStruct(variableToBeAdded, producao, 0, 0);
                        D.SList.Add(state);
                        predict(D, state); // RECURSÃO!!!!!

                    }

                }

            }
        }

        private void scan()
        {

        }

        private void createInitial()
        {
            //List<StateStruct> D0 = new List<StateStruct>();

            DClass D0 = new DClass();

            StateStruct D0Initial = new StateStruct(gramatica.inicial, gramatica.regrasDeProducao[gramatica.inicial][0], 0, 0);

            D0.SList.Add(D0Initial);

            predict(D0, D0Initial);

            DList.Add(D0);

            //string variableToBeAdded = D0Initial.rightSide[D0Initial.pointer];
            //bool alreadyInAState = false;

            //if (gramatica.isVariavel(variableToBeAdded))
            //{
            //    foreach(StateStruct ss in D0.SList)
            //    {
            //        if(string.Equals(ss.leftSide,variableToBeAdded))
            //        {
            //            alreadyInAState = true;
            //        }
            //    }

            //    if (!alreadyInAState)
            //    {
            //        foreach (List<string> producao in gramatica.regrasDeProducao[variableToBeAdded])
            //        {
            //            StateStruct state = new StateStruct(variableToBeAdded, producao, 0, 0);
            //            D0.SList.Add(state);


            //        }
            //    }

            //    //StateStruct D = new StateStruct(D0Initial.rightSide[D0Initial.pointer],);
            //}

            //foreach (string prod in D0Initial.rightSide)
            //{

            //}  //codigo não usado

        }

        public void printAllDs()
        {
            foreach (DClass d in DList)
            {
                foreach(StateStruct ss in d.SList)
                {
                    Console.Write("{0} >", ss.leftSide);

                    foreach(string s in ss.rightSide)
                    {
                        Console.Write(" {0}", s);
                    }
                    Console.WriteLine();
                }
            }
        }

        public void parseGrammar()
        {
            createInitial();
        }

    }


}
