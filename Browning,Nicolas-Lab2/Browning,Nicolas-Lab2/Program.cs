using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Browning_Nicolas_Lab2
{
    class Program
    {
        static void Main(string[] args)
        {

            //main stuff

            string[] options = { "Bubble sort", "Merge sort", "Binary Search", "Save", "Exit" };
            bool t = true;
            string haha;

            int choice = 0;
            List<string> comics = fileLoader();
            List<string> sorted = comics;

            while (choice != 5)
            {
                choice = ReadChoice("algorithm", options);
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("You've chosen option 1\n");
                        Console.WriteLine("Bubble Sort");
                        Console.WriteLine("----------------------------------------------------------------");
                        ///*
                        for (int i = 1; (i <= (sorted.Count - 1)) && t; i++)
                        {
                            t = false;
                            for (int j = 0; j < (sorted.Count - 1); j++)
                            {
                                if (sorted[j + 1].CompareTo(sorted[j]) < 0)
                                {
                                    haha = sorted[j];
                                    sorted[j] = sorted[j + 1];
                                    sorted[j + 1] = haha;
                                    t = true;
                                }
                            }
                        }
                        //*/
                        comics = fileLoader();
                        for (int i = 0; i < comics.Count; i++)
                        {
                            Console.Write($"\n{comics[i],-45}");
                            Console.Write("{0}", sorted[i]);
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("You've chosen option 2\n");
                        Console.WriteLine("Merge Sort");
                        Console.WriteLine("----------------------------------------------------------------");
                        sorted = mergeSort(comics);
                        ///*
                        for (int i = 0; i < sorted.Count; i++)
                        {
                            Console.Write($"\n{comics[i],-45}");
                            Console.Write("{0}", sorted[i]);
                        }
                        //*/
                        //Console.WriteLine("\n" + sorted.Count);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("You've chosen option 3\n");
                        sorted.Sort();
                        BinarySearch(sorted);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("You've chosen option 4\n");
                        sorted.Sort();
                        string name = null;
                        ReadString("new file name", ref name);
                        if (Path.HasExtension(name) == false)
                        {
                            name = Path.ChangeExtension(name, ".json");
                        }
                        Console.WriteLine($"You're new file will be named {name}");
                        WriteJson(name, sorted);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Exiting...");
                        break;
                }
            }

            Console.ReadKey();

        }

        //new methods
        private static int ReadInteger(string promt, int min, int max)
        {
            Console.Write($"Give me a number between {min} and {max} for the {promt}...  ");
            int a;
            do
            {
                try////perfect loop for preventing crashes
                {
                    a = int.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.Write("\nThats not a number, do it right...  ");
                }
            } while (true);
            while (a > max || a < min)
            {
                Console.Write("\nInput exceeds expected range, retype input...  ");
                a = int.Parse(Console.ReadLine());
            }
            return a;
        }

        private static int ReadChoice(string promt, string[] options)
        {
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($" {i + 1}. {options[i]} ");
            }
            Console.Write($"\nSelect your {promt}. ");
            return ReadInteger(promt, 1, options.Length);
        }

        private static List<string> fileLoader()
        {
            List<string> a = new List<string>();
            string what = "aaa";
            using (StreamReader sr = new StreamReader("inputFile.csv"))
            {
                what = sr.ReadToEnd();
            }
            string[] temp = what.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < temp.Length; i++)
            {
                a.Add(temp[i]);
            }
            return a;
        }

        static List<string> mergeSort(List<string> a)
        {
            if (a.Count <= 1)
            {
                return a;
            }
            List<string> left = new List<string>();
            List<string> right = new List<string>();
            for (int i = 0; i < a.Count; i++)
            {
                if (i < (a.Count)/2)
                {
                    left.Add(a[i]);
                }
                else
                {
                    right.Add(a[i]);
                }
            }
            left = mergeSort(left);
            right = mergeSort(right);
            return merge(left, right);
        }

        static List<string> merge(List<string> l, List<string> r)
        {
            List<string> result = new List<string>();
            while (l.Count != 0 && r.Count != 0)
            {
                if (l[0].CompareTo(r[0]) < 0)
                {
                    result.Add(l[0]);
                    l.Remove(l[0]);
                }
                else
                {
                    result.Add(r[0]);
                    r.Remove(r[0]);
                }
            }
            while (l.Count != 0)
            {
                result.Add(l[0]);
                l.Remove(l[0]);
            }
            while (r.Count != 0)
            {
                result.Add(r[0]);
                r.Remove(r[0]);
            }
            return result;
        }
        static void BinarySearch(List<string> a)
        {
            for (int i = 0; i < a.Count; i++)
            {
                Console.Write($"\n{a[i],-45}");
                Console.Write($"Index: {i, -10}");
                Console.Write("Found Index: " + BinarySearch(0, a.Count - 1, a[i], a));
            }
        }

        static int BinarySearch(int low, int high, string term, List<string> a)
        {
            if (high < low)
            {
                return -1;
            }
            int mid = (low + high) / 2;
            if (a[mid].CompareTo(term) > 0)
            {
                return BinarySearch(low, mid - 1, term, a);
            }
            else if (a[mid].CompareTo(term) < 0)
            {
                return BinarySearch(mid + 1, high, term, a);
            }
            else
            {
                return mid;
            }
            /*
            for (int i = low; i < high; i++)
            {
                if (a[i] == term)
                {
                    return i;
                }
            }
            */
        }

        private static void ReadString(string promt, ref string value)
        {
            Console.Write($"Tell me your {promt}...  ");
            value = Console.ReadLine();
            while (string.IsNullOrEmpty(value))
            {
                Console.Write("\nMake sure you type something...  ");
                value = Console.ReadLine();
            }
        }

        static void WriteJson(string filePath, List<string> a)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                using (JsonTextWriter jtw = new JsonTextWriter(sw))
                {
                    jtw.Formatting = Formatting.Indented;
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(jtw, a);
                }
            }
        }
    }
}
