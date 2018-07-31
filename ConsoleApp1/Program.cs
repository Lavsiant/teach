using System;

namespace ConsoleApp1
{
    enum C
    {
        Active
    }
    class Program
    {
        static void Main(string[] args)
        {

            string s = "Active";
            C c;
            Enum.TryParse(s, out c);
            Console.WriteLine((int)c);
        }
    }
}
