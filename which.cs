using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CmdUtils
{
    class which
    {
        static void Main(string[] args)
        {
            var result = Environment.GetEnvironmentVariable("PATH")
                .Split(';')
                .Where(dir => dir != null && dir != "")
                .ToList()
                .FirstOrDefault(dir => System.IO.File.Exists(dir + "\\" + args[0]));

            Console.WriteLine(
                (result != null)
                ? "command found at " + result + "\\" + args[0]
                : "command not found");
        }
    }
}
