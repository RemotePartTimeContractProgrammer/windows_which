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
            if (args.Length != 1)
            {
                Console.WriteLine("no command specified");
                return;
            }

            string command = args[0];

            var path_dirs = Environment.GetEnvironmentVariable("PATH")
                .Split(';')
                .Where(dir => dir != null && dir != "")
                .ToList();

            var first = path_dirs
                .FirstOrDefault(dir => System.IO.File.Exists(dir + "\\" + command));

            Console.WriteLine(
                (first != null)
                ? "command found at " + first + "\\" + command
                : "command not found");
        }
    }
}
