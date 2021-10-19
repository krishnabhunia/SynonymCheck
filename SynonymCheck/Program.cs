using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynonymCheck
{
    public class Pair
    {
        public string key { get; set; }
        public string value { get; set; }

        public Pair(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }

    class Program
    {
        static List<Pair> keyValuePairs;
        static List<string> output;

        static void Main(string[] args)
        {
            Console.WriteLine("Reading Input File");
            output = new List<string>();
            string text = File.ReadAllText(@"C:\Users\Krishna Bhunia\source\repos\SynonymCheck\SynonymCheck\Input\test.in");
            //C:\Users\Krishna Bhunia\source\repos\SynonymCheck\SynonymCheck\Input\test.in

            string strInitialPath = Environment.CurrentDirectory;
            text = $@"{Path.GetDirectoryName(Path.GetDirectoryName(strInitialPath))}\Input\test.in";

            if (Debugger.IsAttached)
            {
                text = @"C:\Users\Krishna Bhunia\source\repos\SynonymCheck\SynonymCheck\Input\example_big.in";
                //text = @"C:\Users\Krishna Bhunia\source\repos\SynonymCheck\SynonymCheck\Input\mysample.in";
            }

            string[] lines = File.ReadAllLines(text);
            Console.WriteLine(text);

            int int_query = Int32.Parse(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                Console.WriteLine("Process Started");
                if (Int32.TryParse(lines[i], out int dict))
                {
                    keyValuePairs = new List<Pair>();

                    for (int j = i + 1; j <= dict + i; j++)
                    {
                        string[] pair = lines[j].Split(' ');
                        if (pair[0].ToLower() != pair[1].ToLower())
                        {
                            keyValuePairs.Add(new Pair(pair[0].ToLower(), pair[1].ToLower()));
                            keyValuePairs.Add(new Pair(pair[1].ToLower(), pair[0].ToLower()));
                        }

                    }
                    do
                    {
                        Console.WriteLine("Do While Process");
                    } while (Process());
                    Console.WriteLine("Process Done");

                    i += (dict + 1);
                }

                Console.WriteLine("Synonym Dictionary Check");
                if (Int32.TryParse(lines[i], out int res))
                {
                    for (int j = i + 1; j <= i + res; j++)
                    {
                        string[] result = lines[j].Split(' ');
                        if (result[0].ToLower() == result[1].ToLower())
                        {
                            output.Add("synonyms");
                        }
                        else
                        {
                            if (GetValueFromDict(result[0], result[1]) || GetValueFromDict(result[1], result[0]))
                            {
                                output.Add("synonyms");
                            }
                            else
                            {
                                output.Add("different");
                            }
                        }
                    }
                    i += res;
                }
                Console.WriteLine("Synonym Dictionary Done");
            }
            Console.WriteLine("Output File Writing");
            WriteOutput();
            Console.WriteLine("Output File Saved");
        }

        private static void WriteOutput()
        {
            string strInitialPath = Environment.CurrentDirectory;
            string text = $@"{Path.GetDirectoryName(Path.GetDirectoryName(strInitialPath))}\Output\test.out";
            File.WriteAllLines(text, output);
        }

        private static bool GetValueFromDict(string key, string val)
        {
            var r1 = keyValuePairs.Where(x => x.key.ToLower() == key.ToLower() & x.value.ToLower() == val.ToLower());
            var r11 = keyValuePairs.Where(x => x.key.ToLower() == key.ToLower());
            var r12 = keyValuePairs.Where(x => x.value.ToLower() == val.ToLower());
            if (r1.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Process()
        {
            bool pro = false;
            foreach (var p in keyValuePairs.Cast<Pair>().ToArray())
            {
                IEnumerable<Pair> t1 = keyValuePairs.Where(x => (x.key.ToLower() == p.value.ToLower() && (x.value.ToLower() != p.key.ToLower())));
                if (t1.Count() > 0)
                {
                    foreach(var t11 in t1.Cast<Pair>().ToArray())
                    {
                        var k = p.key.ToLower();
                        var v = t11.value.ToLower();
                        if (k != v & PairExits(k, v))
                        {
                            keyValuePairs.Add(new Pair(k, v));
                            keyValuePairs.Add(new Pair(v, k));
                            pro = true;
                        }
                    }
                }
            }
            return pro;
        }

        static bool PairExits(string k, string v)
        {
            var t1 = keyValuePairs.Where(x => (x.key.ToLower() == k.ToLower() & x.value.ToLower() == v.ToLower())
            | (x.value.ToLower() == k.ToLower() & x.key.ToLower() == v.ToLower()));
            if (t1.Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
