using Ditch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class GolosManager
    {
        public GolosManager(string username, string password)
        {
        }

        public static ulong GetUsersCount()
        {
            var chain = new Dictionary<string, ChainInfo>();
            var golosChainInfo = ChainManager.GetChainInfo(KnownChains.Golos);
            chain.Add("Golos", golosChainInfo);
            var Golos = new OperationManager(golosChainInfo.Url, golosChainInfo.ChainId);
            return Golos.GetAccountCount().Result;
        }

    }
}
