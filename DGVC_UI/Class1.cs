using ConsoleApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGVC_UI
{
    class Class1
    {
        public static void im(string s)
        {

            string[] input = s.Split(' ');
            string command = input[0];
            if (command == "exit")
            {
               
            }
            else
            {
                List<string> commandArgs = new List<string>();
                for (uint i = 1; i < input.Length; i++)
                {
                    commandArgs.Add(input[i]);
                }
                DGShellExecutor.Execute(command, commandArgs);
            }
        }
    }
}
