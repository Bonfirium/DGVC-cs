using System;
using System.Configuration;

namespace TestProject
{
    class Program
    {
        private static string UserPrivateKey => ConfigurationManager.AppSettings["UserPrivateKey"];

        static void Main(string[] args)
        {

            //Console.WriteLine("Enter the username");
            //string username = Console.ReadLine();
            //Console.WriteLine("Enter the password");
            //string password = Console.ReadLine();
            Console.WriteLine("Account Count = " + GolosManager.GetUsersCount());
            //var accountHistory = Golos.GetAccountHistory("vermilliest", 1000, 1000);
            //foreach (var a in accountHistory.Result)
            //{
            //    if ((a.Value.Op[0] as string) == "account_create")
            //    {
            //        Console.WriteLine("Account create");
            //        Console.WriteLine("\tBlock #" + a.Value.Block);
            //    }
            //}

#if DEBUG
            Console.Read();
#endif
        }
    }
}
