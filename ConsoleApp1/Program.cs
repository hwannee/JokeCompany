﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static string[] results = new string[50];
        static char key;
        static Tuple<string, string> name;
        static ConsolePrinter printer = new ConsolePrinter();

        static void Main(string[] args)
        {
            printer.Value("Press h to get instructions.").ToString();
            GetEnteredKey(Console.ReadKey());

            if(key == 'h')
            {
                while (true)
                {
                    printer.Value(Environment.NewLine + "Press c to get categories").ToString();
                    printer.Value("Press r to get random jokes").ToString();
                    GetEnteredKey(Console.ReadKey());
                    if (key == 'c')
                    {
                        GetCategories();
                        PrintResults();
                    }
                    if (key == 'r')
                    {
                        printer.Value(Environment.NewLine + "Want to use a random name? y/n").ToString();
                        GetEnteredKey(Console.ReadKey());
                        if (key == 'y')
                        {
                            GetNames();
                            printer.Value(Environment.NewLine + name).ToString();
                        }
                        else
                        {
                            printer.Value(Environment.NewLine + "Enter the name you want to use.").ToString();
                            string s = Console.ReadLine();
                            string firstName = s.Substring(0, s.LastIndexOf(' '));
                            string lastName = s.Substring(s.LastIndexOf(' ') + 1);
                            name = Tuple.Create(firstName, lastName);
                        }                        
                            
                        printer.Value("Want to specify a category? y/n").ToString();
                        GetEnteredKey(Console.ReadKey());
                        if (key == 'y')
                        {
                            // TODO: modularize
                            printer.Value(Environment.NewLine + "How many jokes do you want? (1-9)cccc").ToString();
                            int n = Int32.Parse(Console.ReadLine());
                            // TODO: handle boundary case
                            // TODO: fix category
                            printer.Value("Enter a category;").ToString();
                            GetRandomJokes(Console.ReadLine(), n);
                            PrintResults();
                        }
                        else 
                        {
                            printer.Value(Environment.NewLine + "How many jokes do you want? (1-9)").ToString();
                            int n = Int32.Parse(Console.ReadLine());
                            // handle boundary case
                            GetRandomJokes(null, n);
                            PrintResults();
                        }
                    }
                    name = null;
                }
            }

        }

        private static void PrintResults()
        {
            for(int i = 0; i < results.Length; i++)
            {
                printer.Value("[" + string.Join(",", results[i]) + "]").ToString();
            }
        }

        private static void GetEnteredKey(ConsoleKeyInfo consoleKeyInfo)
        {
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.C:
                    key = 'c';
                    break;
                case ConsoleKey.D0:
                    key = '0';
                    break;
                case ConsoleKey.D1:
                    key = '1';
                    break;
                case ConsoleKey.D3:
                    key = '3';
                    break;
                case ConsoleKey.D4:
                    key = '4';
                    break;
                case ConsoleKey.D5:
                    key = '5';
                    break;
                case ConsoleKey.D6:
                    key = '6';
                    break;
                case ConsoleKey.D7:
                    key = '7';
                    break;
                case ConsoleKey.D8:
                    key = '8';
                    break;
                case ConsoleKey.D9:
                    key = '9';
                    break;
                case ConsoleKey.R:
                    key = 'r';
                    break;
                case ConsoleKey.Y:
                    key = 'y';
                    break;
                case ConsoleKey.N:
                    key = 'n';
                    break;
                case ConsoleKey.H:
                    key = 'h';
                    break;
            }
        }

        private static void GetRandomJokes(string category, int number)
        {            
            new JsonFeed("http://api.icndb.com", number);
            results = JsonFeed.GetRandomJokes(name?.Item1, name?.Item2, category);
        }

        private static void GetCategories()
        {
            new JsonFeed("http://api.icndb.com", 0);
            results = JsonFeed.GetCategories();
        }

        private static void GetNames()
        {
            new JsonFeed("http://uinames.com/api", 0);
            dynamic result = JsonFeed.GetName();
            name = Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
    }
}
