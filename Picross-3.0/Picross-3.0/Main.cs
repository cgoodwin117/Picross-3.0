using System;

namespace Picross_3._0
{
    class Program
    {
        static void Main(string[] args)
        {
            PicrossGame test = new PicrossGame(@"/Users/curtthedirt/Desktop/Code/Picross-3.0/Picross-3.0/Picross-3.0/input.txt");
            Console.WriteLine(test);
        }
    }
}
