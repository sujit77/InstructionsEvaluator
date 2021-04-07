using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace InstructionsEvaluator
{
    public class FileToDictionaryConverter
    {
        public Dictionary<string,string> ReadInstructionsFromFileIntoDict(string path)
        {
            string[] lines = File.ReadAllLines(path);
            return lines.Select(line => line.Split(':')).GroupBy(arr => arr[0]).ToDictionary(g => g.Key, g => g.First()[1]);
           
        }
    }
}
