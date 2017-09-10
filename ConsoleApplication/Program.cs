using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TestProject;

namespace ConsoleApplication {
    class Program {

        public static readonly GolosManager GolosManager = new GolosManager( );

        static void Main(string[ ] args) {
            bool printCurrentDirectory = true;
#if DEBUG
            for (uint i = 0; i < 3; i++) {
                DGShellExecutor.Execute("cd", new List<string>( ) { ".." });
            }
#endif
            while (true) {
                Console.WriteLine( );
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (printCurrentDirectory) {
                    Console.WriteLine(" " + Directory.GetCurrentDirectory( ));
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" >> ");
                Console.ForegroundColor = ConsoleColor.Gray;
                string[ ] input = Console.ReadLine( ).Split(' ');
                string command = input[0];
                if (command == "exit") {
                    break;
                } else {
                    List<string> commandArgs = new List<string>( );
                    for (uint i = 1; i < input.Length; i++) {
                        commandArgs.Add(input[i]);
                    }
                    printCurrentDirectory = DGShellExecutor.Execute(command, commandArgs);
                }
            }
            //#if DEBUG
            //            Console.ReadLine();
            //#endif
        }
    }
}
