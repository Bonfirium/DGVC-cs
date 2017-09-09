//#define FROM_EXAMPLE

using Ditch;
using Ditch.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace TestProject
{
    class Program
    {
        private static string UserPrivateKey => ConfigurationManager.AppSettings["UserPrivateKey"];

        static void Main(string[] args)
        {

            Console.WriteLine("Enter the username");
            string username = Console.ReadLine();
            Console.WriteLine("Enter the password");
            string password = Console.ReadLine();
#if FROM_EXAMPLE
            ChainInfo Chain = ChainManager.GetChainInfo(KnownChains.Steem);
            var OperationManager = new OperationManager(Chain.Url, Chain.ChainId, Chain.JsonSerializerSettings);
            var YouPrivateKeys = new List<byte[]>
            {
                Base58.GetBytes(UserPrivateKey) //WIF
            };
            var YouLogin = "vermilliest";
            var a = OperationManager.GetAccountCount();
            Console.WriteLine("Accaount count = " + a);
#else
            var Login = new Dictionary<string, string>() { { "Golos", username } };
            var UserPrivateKeys = new Dictionary<string, List<byte[]>>() {
                { "Golos", new List<byte[]> { Base58.TryGetBytes(password) } },
            };
            var Chain = new Dictionary<string, ChainInfo>();
            var golosChainInfo = ChainManager.GetChainInfo(KnownChains.Golos);
            Chain.Add("Golos", golosChainInfo);
            var Golos = new OperationManager(golosChainInfo.Url, golosChainInfo.ChainId);
            var accountCount = Golos.GetAccountCount();
            Console.WriteLine("Account Count = " + accountCount.Result);
            var accountHistory = Golos.GetAccountHistory("vermilliest", 1000, 1000);
            foreach (var a in accountHistory.Result)
            {
                if ((a.Value.Op[0] as string) == "account_create")
                {
                    Console.WriteLine("Account create");
                    Console.WriteLine("\tBlock #" + a.Value.Block);
                }
            }
#endif

#if DEBUG
            Console.Read();
#endif
        }
    }
}
