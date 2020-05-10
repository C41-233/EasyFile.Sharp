using System;
using System.Collections.Generic;
using EasyFile;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = new File("test.txt");
            Console.Write(file.Size);
        }
    }
}
