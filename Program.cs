using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Transpose
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> inTable = new List<List<int>>();

            try
            {   
                var inFile = new FileStream("in.txt", FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(inFile, Encoding.UTF8))
                {
                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        List<int> row = new List<int>();
                        string[] words = line.Split(' ');

                        foreach (var word in words)
                        {
                            row.Add(Int32.Parse(word));
                        }
                        inTable.Add(row);
                    }
                    streamReader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            List<List<int>> transposed = Enumerable.Range(0, inTable.Max(list => list.Count))
                .Select(i => inTable.Select(list => list.ElementAtOrDefault(i)).ToList())
                .ToList();


            try
            {
                var outFile = new FileStream("out.txt", FileMode.Create, FileAccess.Write);
                using (
                StreamWriter streamWriter = new StreamWriter(outFile))
                {

                    transposed.ForEach(i =>
                               {
                                   i.ForEach(j =>
                                   {
                                       if (j == 0)
                                       {
                                           streamWriter.Write("\t");
                                       }
                                       else
                                       {
                                           streamWriter.Write("{0}\t", j);
                                       }
                                   });
                                   streamWriter.WriteLine();
                               });
                    streamWriter.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
