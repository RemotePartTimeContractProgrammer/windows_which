using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace which
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
                throw new Exception("Usage: which <command>");


            System.Text.StringBuilder str = new StringBuilder();
            var path_dirs = Environment.GetEnvironmentVariable("PATH").Split(';');
            var user_arg = args[0];
            var search_string = (user_arg.Contains(".") ? user_arg : "*" + user_arg + "*");
            bool error_occurred = false;
            string live_indicator = " <-- Active";
            foreach (var dir in path_dirs)
            {
                try
                {
                    var files = new DirectoryInfo(dir).GetFiles(search_string);

                    foreach (FileInfo file in files)
                    {
                        str = str.AppendLine(file.FullName + live_indicator);
                        live_indicator = "";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException == null ? ex.Message : ex.Message + "\n" + ex.InnerException.Message);
                    error_occurred = true;
                }

            }
            if (error_occurred)
                Console.WriteLine("\n");
            Console.WriteLine("\n------\n");
            Console.WriteLine(str.ToString());
        }
    }
}
