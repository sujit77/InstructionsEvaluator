using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Concurrent;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace InstructionsEvaluator
{
    class Program
    {
        private static List<string> ops = new List<string>() { "Add", "Mult" };
        private static string filePath = "inputMaster.txt"; // this can be configured in app.config or can be driven by unit test project
        private static Stack myRunningResultStack = new Stack(); // stack to keep track of data in operations 
        private static Stack myOriginalStack = new Stack();
        public static int finalResult;
       
        static void Main(string[] args)
        {
            //step 1: read the input file and create dictionary data structure
          
            var fileToDictionaryConverter = new FileToDictionaryConverter();
            var dict = fileToDictionaryConverter.ReadInstructionsFromFileIntoDict(filePath);
            var dictWithArray = new Dictionary<string, string[]>();
            foreach (KeyValuePair<string, string> kv in dict)
            {
                dictWithArray[kv.Key] = kv.Value.TrimStart().Split();
            }

            //step 2: get the first row from text file and clear dictionary to save memory
            var firstInst = dictWithArray[dict.Take(1).Select(d => d.Key).First()];
            dict.Clear();
            dict = null;


            // step 3: push all items in first row 
            foreach (string s in firstInst)
            {
                myOriginalStack.Push(s);
            }

            // step 4: maintain list of numbers that needs to be added or multiplied together till next operation is reached
            List<int> currentItems = new List<int>();
            while (myOriginalStack.Count != 0)
            {
                var item = myOriginalStack.Pop();

                if (ops.Contains(item.ToString()))
                {
                    DoProcessing(item.ToString());
                    continue;
                }
                var items = dictWithArray[item.ToString()];
                if (items[0] == "Value")
                {
                    myRunningResultStack.Push(int.Parse(items[1].ToString()));
                    if (!ops.Contains(myOriginalStack.Peek()))
                    {
                        continue;
                    }
                    var operationPop = myOriginalStack.Pop().ToString();
                    DoProcessing(operationPop);

                }
                else
                {
                    myOriginalStack.Push(items[0]);
                    myRunningResultStack.Push(items[0]);
                    for (int i = items.Length - 1; i > 0; i--)
                    {
                        myOriginalStack.Push(items[i]);
                    }

                }

            }
            Console.WriteLine(finalResult);
            Console.ReadKey();
        }



        private static void DoProcessing(string operationPop)
        {
            // Need to refactor this code to get rid of if else if as it violates "O" os SOLID but does the job for now
            if (operationPop == "Add")
            {
                int sum = 0;
                if (myOriginalStack.Count == 0)
                {
                    // special treatment when we reach the end of the original stack we cannot peek any because all are popped
                    while (myRunningResultStack.Count > 0)
                    {
                        sum = int.Parse(myRunningResultStack.Pop().ToString()) + sum;
                    }
                    finalResult = sum;
                }
                else
                {
                    while (myRunningResultStack.Peek().ToString() != "Add")
                    {
                        sum = int.Parse(myRunningResultStack.Pop().ToString()) + sum;
                    }
                }
                if (myOriginalStack.Count > 0)
                {
                    // pop previously calculated result and replace it by pushing new calculated result
                    myRunningResultStack.Pop();
                    myRunningResultStack.Push(sum);
                }
            }
            else if (operationPop == "Mult")
            {
                var mult = 1;
                if (myOriginalStack.Count == 0)
                {
                    // special treatment when we reach the end of the original stack we cannot peek any because all are popped
                    while (myRunningResultStack.Count > 0)
                    {
                        mult = int.Parse(myRunningResultStack.Pop().ToString()) * mult;
                    }
                    finalResult = mult;
                }
                while (myRunningResultStack.Peek().ToString() != "Mult")
                {
                    mult = int.Parse(myRunningResultStack.Pop().ToString()) * mult;
                }
                if (myOriginalStack.Count > 0)
                {
                    // pop previously calculated result and replace it by pushing new calculated result
                    myRunningResultStack.Pop();
                    myRunningResultStack.Push(mult);
                }
            }
        }

    }
}
