using System;

namespace CmdUtils
{
    class MainDriver
    {
        static void Usage()
        {
            throw new Exception("which [-p] command");
        }

        static void Main(string[] args)
        {
            string command = null;
            bool show_paths = false;

            for (var i = 0; i < args.Length; i++)
            {
                if (args[i][0] == '-')
                {
                    switch (args[i][1])
                    {
                        case 'p': show_paths = true; break;
                        default: throw new Exception("Illegal argument: " + args[i]);
                    }
                }
                else
                {
                    if (command != null)
                        Usage();

                    command = args[i];
                }
            }

            which.Resolve(command, show_paths);
        }
    }
}
