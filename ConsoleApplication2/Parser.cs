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
        public List<string> AlreadyCompletedList = new List<string>();
    }

    class Parser
    {
        List<DClass> DList = new List<DClass>();
        Gramatica gramatica;
        int inputPointer;

        bool success;


        public Parser(Gramatica gramatica)
        {
            this.gramatica = gramatica;
            this.inputPointer = 0;
            success = false;

            createInitial();
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
                        StateStruct state = new StateStruct(variableToBeAdded, producao, 0, inputPointer);
                        D.SList.Add(state);
                        predict(D, state); // RECURSÃO!!!!!

                    }

                }

            }
        }

        private void scan(DClass D, string token)
        {
            if (gramatica.isTerminal(token))
            {
                foreach (StateStruct state in DList[inputPointer - 1].SList)
                {
                    if(state.pointer < state.rightSide.Count)
                    {
                        if(String.Equals(state.rightSide[state.pointer], token))
                        {
                            StateStruct nState = new StateStruct(state.leftSide, state.rightSide, state.pointer + 1, state.origin);
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

        private void complete(DClass D, StateStruct stateToComplete)
        {
            string variableToComplete = stateToComplete.leftSide;
            bool alreadyCompleted = false;

            foreach (string variable in D.AlreadyCompletedList)
            {
                if (string.Equals(variableToComplete, variable))
                {
                    alreadyCompleted = true;
                }
            }

            if (!alreadyCompleted)
            {
                foreach (StateStruct state in DList[stateToComplete.origin].SList)
                {
                    if (state.pointer < state.rightSide.Count)
                    {
                        if (String.Equals(state.rightSide[state.pointer], variableToComplete))
                        {
                            StateStruct nState = new StateStruct(state.leftSide, state.rightSide, state.pointer + 1, state.origin);
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

        private bool checkSuccess()
        {
            foreach (StateStruct state in DList[inputPointer - 1].SList)
            {
                if ((String.Equals(state.leftSide, gramatica.inicial)) && state.origin == 0 && (state.pointer >= state.rightSide.Count))
                {
                    success = true;
                    
                }
            }

            return success;

        }

        public bool getSuccess()
        {
            return success;
        }

        public void parseSentence(string[] inputStringArray)
        {

            for (inputPointer = 1; inputPointer < inputStringArray.Length + 1; inputPointer++)
            {
                DClass D = new DClass();

                scan(D, inputStringArray[inputPointer - 1]);

                DList.Add(D);
            }

            checkSuccess();

        }

        private List<string> getTerminalsFromPreviousDClass(DClass D, int I)
        {

            List<string> Relevantes = new List<string>();

            foreach (StateStruct State in DList[I - 1].SList)
            {
                if (State.pointer < State.rightSide.Count)
                {
                    if (gramatica.terminais.Contains(State.rightSide[State.pointer]))
                        Relevantes.Add(State.rightSide[State.pointer]);
                }
            }

            return Relevantes;
        }

        public string generateSentence(int size)
        {
            string randomSentence = "";
            string randomTerminal = "";

            Random rng = new Random();

            inputPointer = 1;
            while ((inputPointer < size +1) || (!checkSuccess()))
            {

                DClass D = new DClass();

                List<string> terminalsFromPreviousD = getTerminalsFromPreviousDClass(D, inputPointer);

                randomTerminal = terminalsFromPreviousD[rng.Next(terminalsFromPreviousD.Count)];
                randomSentence += randomTerminal;

                scan(D, randomTerminal);

                DList.Add(D);

                inputPointer++;
            }

            //for (inputPointer = 1; inputPointer < size + 1; inputPointer++)
            //{
            //    DClass D = new DClass();

            //    List<string> terminalsFromPreviousD = getTerminalsFromPreviousDClass(D, inputPointer);

            //    randomTerminal = terminalsFromPreviousD[rng.Next(terminalsFromPreviousD.Count)];
            //    randomSentence += randomTerminal;

            //    scan(D, randomTerminal);

            //    DList.Add(D);
            //}

            return randomSentence;
        }


        public void printAllDs()
        {
            int i = 0; 
            foreach (DClass d in DList)
            {
                Console.WriteLine("D{0}:", i);

                foreach(StateStruct ss in d.SList)
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
