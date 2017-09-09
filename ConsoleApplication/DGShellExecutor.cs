using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication {
    public static class DGShellExecutor {

        public static bool HasExtention(string filename, string extention) {
            string extentionWithDot = "." + extention;
            return filename.LastIndexOf(extentionWithDot) == filename.Length - extentionWithDot.Length;
        }

        public static bool HasExtention(string filename, List<string> extentions) {
            foreach (var extention in extentions) {
                if (HasExtention(filename, extention)) {
                    return true;
                }
            }
            return false;
        }

        public static bool IsSource(string filename) {
            return HasExtention(filename, new List<string>( ) {
                "cs",
                "cpp",
                "js",
                "py",
                "sln",
                "csproj",
                "config",
            });
        }

        public static bool Execute(string command, List<string> arguments) {
            try {
                switch (command) {
                    case "cd":
                        if (arguments[0] == "..") {
                            string currentDirectory = Directory.GetCurrentDirectory( );
                            string destinationDirectory = currentDirectory.Substring(0, currentDirectory.LastIndexOf('\\'));
                            if (destinationDirectory.Length == 2) {
                                destinationDirectory += '\\';
                            }
                            Directory.SetCurrentDirectory(destinationDirectory);
                            return true;
                        } else if (arguments[0][1] != ':') {
                            Directory.SetCurrentDirectory(Directory.GetCurrentDirectory( ) + '\\' + arguments[0]);
                            return true;
                        } else if (arguments[0].Substring(1, 2) == ":\\") {
                            Directory.SetCurrentDirectory(arguments[0]);
                            return true;
                        }
                        break;
                    case "ls":
                        var process = new Process {
                            StartInfo = new ProcessStartInfo {
                                FileName = "cmd.exe",
                                Arguments = "/c dir /b",
                                RedirectStandardOutput = true,
                                UseShellExecute = false,
                                CreateNoWindow = true,
                            }
                        };
                        process.Start( );
                        ConsoleColor prevColor = Console.ForegroundColor;
                        while (!process.StandardOutput.EndOfStream) {
                            string result = process.StandardOutput.ReadLine( );
                            if (Directory.Exists(Directory.GetCurrentDirectory( ) + '\\' + result)) {
                                Console.ForegroundColor = ConsoleColor.Green;
                                result += '\\';
                            } else if (result == ".gitignore" || result == ".gitconfig") {
                                Console.ForegroundColor = ConsoleColor.Red;
                            } else if (IsSource(result)) {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                            } else {
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            Console.WriteLine("\t" + result);
                        }
                        Console.ForegroundColor = prevColor;
                        return true;
                }
            } catch {
            }
            return false;
        }
    }
}
