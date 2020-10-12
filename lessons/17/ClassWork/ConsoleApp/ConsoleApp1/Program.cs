using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var generator = new Generator();
            generator.DataGenerated += (sender, data) => File.WriteAllBytes("file", data.Data);

            generator.Generate(100);
        }
    }
}
