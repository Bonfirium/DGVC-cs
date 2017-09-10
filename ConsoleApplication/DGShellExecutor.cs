using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject;

namespace ConsoleApplication {
    public static class DGShellExecutor {

        private static Process _process = null;

        private static List<string> ExecuteCMDCommand(string command) {
            if (_process != null) {
                _process.Dispose( );
                _process = null;
            }
            _process = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = "cmd.exe",
                    Arguments = "/c " + command,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                }
            };
            _process.Start( );
            using (StreamWriter pWriter = _process.StandardInput) {
                if (pWriter.BaseStream.CanWrite) {
                    pWriter.WriteLine(_process.StartInfo.Arguments);
                }
            }
            List<string> result = new List<string>( );
            while (!_process.StandardOutput.EndOfStream) {
                result.Add(_process.StandardOutput.ReadLine( ));
            }
            return result;
        }

        public static string Sum(this IEnumerable<string> collection, string split="") {
            string result = "";
            bool isFirst = true;
            foreach (var item in collection) {
                if (!isFirst) {
                    result += split;
                } else {
                    isFirst = false;
                }
                result += item;
            }
            return result;
        }

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
                            //TODO: Make space control
                            Directory.SetCurrentDirectory(Directory.GetCurrentDirectory( ) + '\\' + arguments[0]);
                            return true;
                        } else if (arguments[0].Substring(1, 2) == ":\\") {
                            Directory.SetCurrentDirectory(arguments[0]);
                            return true;
                        }
                        break;
                    case "ls": {
                            List<string> @out = ExecuteCMDCommand("dir /b");
                            ConsoleColor prevColor = Console.ForegroundColor;
                            foreach (var a in @out) {
                                string result = a.Clone( ) as string;
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
                    case "mkdir":
                        Directory.CreateDirectory(Directory.GetCurrentDirectory( ) + '\\' + arguments[0]);
                        break;
                    case "git": {
                            List<string> @out;
                            if (arguments[0] == "l") {
                                string commandStr = "git log --format=oneline";
                                for (uint i = 1; i < arguments.Count; i++) {
                                    commandStr += " " + arguments[(int)i];
                                }
                                @out = ExecuteCMDCommand(commandStr);
                            } else {
                                @out = ExecuteCMDCommand("git " + arguments.Sum(" "));
                            }
                            foreach (var a in @out) {
                                Console.WriteLine(a);
                            }
                            return true;
                        }
                    case "dgvc":
                        switch (arguments[0]) {
                            case "login":
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(" !!! Enter your username:");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(" >> ");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                string username = Console.ReadLine( );
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(" !!! Enter your password:");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(" >> ");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                var pass = new Stack<char>( );
                                char chr = (char)0;
                                int[] FILTERED = { 0, 27, 9, 10, 32 }; // const
                                while ((chr = Console.ReadKey(true).KeyChar) != 13) { // 13 is ENTER
                                    if (chr == 8) { // Backspace
                                        if (pass.Count > 0) {
                                            Console.Write("\b \b");
                                            pass.Pop( );
                                        }
                                    } else if (chr == 127) { // Ctrl + Backspace
                                        while (pass.Count > 0) {
                                            Console.Write("\b \b");
                                            pass.Pop( );
                                        }
                                    } else if (FILTERED.Count(x => chr == x) > 0) { } else {
                                        pass.Push(chr);
                                        Console.Write('*');
                                    }
                                }
                                char[] passwordByChars = pass.Reverse( ).ToArray( );
                                string password = "";
                                foreach (var a in passwordByChars) {
                                    password += a;
                                }
                                Program.GolosManager.Login(username, password);
                                Console.WriteLine( );
                                return true;
                            case "post":
                                switch (arguments[1]) {
                                    case "commit":
                                        string shortId = arguments[2];
                                        List<string> shortLog = ExecuteCMDCommand("git log --format=oneline");
                                        bool finded = false;
                                        bool identity = true;
                                        string fullId = null;
                                        foreach (var a in shortLog) {
                                            if (a.IndexOf(shortId) == 0) {
                                                if (finded) {
                                                    identity = false;
                                                    break;
                                                } else {
                                                    finded = true;
                                                    fullId = a.Split(' ')[0];
                                                }
                                            }
                                        }
                                        if (!finded) {
                                            throw new Exception("Commit " + shortId + " does not find");
                                        }
                                        if (!identity) {
                                            throw new Exception("Commit " + shortId + " is not identity");
                                        }
                                        Program.GolosManager.CreatePost("Commit by " + Program.GolosManager.UserName + " #" + fullId, new string[] {
                                            GlobalDGVCSettings.LOGO_W_URL,
                                        }, new string[0]);
                                        break;
                                }
                                break;
                        }
                        break;
                }
            } catch (Exception e) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" ---===ERROR===---");
                Console.WriteLine(" " + e.Message.Replace("\n", "\n "));
            }
            //return false;
            return true;
        }
    }
}
