using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ConsoleApplication {
    class Program {
        static void Main(string[ ] args) {
            bool printCurrentDirectory = true;
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
