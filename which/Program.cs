using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavisSquareUtils
{
    class which
    {
        public static void Resolve(string command_name, bool show_paths)
        {
            /* 
             * if the command name includes a '.', assume full extension is specified and only match on the 
             * exact match (e.g. "notepad.exe")
             * otherwise, check for any eligible matches based on the file extensions in pathext_extensions
             */

            if (command_name.Contains("."))
                exact_match(command_name, show_paths);
            else
                fuzzy_match(command_name);
        }

        static IEnumerable<string> get_path_dirs()
            => Environment.GetEnvironmentVariable("PATH").Split(';');

        static void success(string filepath)
            => Console.WriteLine(filepath);

        static void fail(string command_name)
            => Console.WriteLine("Command: " + command_name + " not found in path.");


        /* user entered command with extension. only match on exact match. 
         * (as opposed to checking <command>.exe, <command>.com, etc...
         */
        static void exact_match(string command_name, bool show_paths)
        {
            foreach (var dir in get_path_dirs())
            {
                if (show_paths)
                    Console.WriteLine("Searching " + dir);

                try
                {
                    var matching_file = new DirectoryInfo(dir)
                        .GetFiles(command_name)
                        .FirstOrDefault();

                    if (matching_file != null)
                    {
                        success(matching_file.FullName);
                        return;
                    }
                }
                catch (Exception ex) { err(ex); }
            }

            fail(command_name);
        }

        static void err(Exception ex)
            => Console.WriteLine(ex.InnerException == null ? ex.Message : ex.Message + "\n" + ex.InnerException.Message + "\n");

        static string [] get_extensions ()
        {
            /* if present, pathext environment variable determines suffix search order 
             * otherwise, use defaults from here 
             * https://stackoverflow.com/questions/605101/order-in-which-command-prompt-executes-files-with-the-same-name-a-bat-vs-a-cmd
             */
            var pathext_environment_variable =
                Environment.GetEnvironmentVariable("PATHEXT");

            return (pathext_environment_variable != null)
                    ? pathext_environment_variable.Split(';')
                    : new string[] { ".com", ".exe", ".bat", ".cmd" }; 
        }

        static void fuzzy_match(string command_name)
        {
            var path_dirs =
                get_path_dirs();

            var path_extensions =
                get_extensions();

            foreach (var dir in path_dirs)
            {
                try
                {
                    var loose_matches = 
                        new DirectoryInfo(dir).GetFiles(command_name + ".*");

                    foreach (var ext in path_extensions)
                    {
                        var exact_match = 
                            loose_matches.SingleOrDefault(f => f.Name.ToLowerInvariant() == (command_name + ext).ToLowerInvariant());

                        if (exact_match != null)
                        {
                            success(exact_match.FullName);
                            return;
                        }
                    }
                }
                catch (Exception ex) { err(ex); }
            }
        }
    }
}
