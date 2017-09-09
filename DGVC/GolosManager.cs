using Ditch;
using Ditch.JsonRpc;
using Ditch.Operations.Get;
using System.Collections.Generic;

namespace TestProject
{
    public class GolosManager
    {
        public OperationManager Golos { get; protected set; }

        public GolosManager()
        {
            var chain = new Dictionary<string, ChainInfo>();
            var golosChainInfo = ChainManager.GetChainInfo(KnownChains.Golos);
            chain.Add("Golos", golosChainInfo);
            Golos = new OperationManager(golosChainInfo.Url, golosChainInfo.ChainId);
        }

        public ulong GetUsersCount()
        {
            return Golos.GetAccountCount().Result;
        }

        private JsonRpcResponse<KeyValuePair<uint, AppliedOperation>[]> GetUserHistoryAsJson(string userName, ulong from, uint limit = uint.MaxValue)
        {
            return Golos.GetAccountHistory(userName, from, limit);
        }

        public List<UserHistoryElement> GetUserHistory(string userName, ulong from, uint limit = uint.MaxValue)
        {
            var json = GetUserHistoryAsJson(userName, from, limit);
            List<UserHistoryElement> result = new List<UserHistoryElement>();
            foreach (var historyElement in json.Result)
            {
                string eventType = historyElement.Value.Op[0] as string;
            }
            return result;
        }
    }
}
