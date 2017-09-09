using Ditch;
using Ditch.JsonRpc;
using Ditch.Operations.Get;
using Newtonsoft.Json.Linq;
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

        private JsonRpcResponse<KeyValuePair<uint, AppliedOperation>[]> GetUserHistoryAsJson(string userName, ulong from, uint limit)
        {
            return Golos.GetAccountHistory(userName, from, limit);
        }

        public List<UserHistoryElement> GetUserHistory(string userName, ulong from, uint limit)
        {
            JsonRpcResponse<KeyValuePair<uint, AppliedOperation>[]> json =
                GetUserHistoryAsJson(userName, from, limit);
            List<UserHistoryElement> result = new List<UserHistoryElement>();
            KeyValuePair<uint, AppliedOperation>[] keyValuePair =
                json.Result as KeyValuePair<uint, AppliedOperation>[];
            foreach (KeyValuePair<uint, AppliedOperation> historyElement in keyValuePair)
            {
                AppliedOperation operation = historyElement.Value;
                uint blockId = operation.Block;
                object[] op = operation.Op;
                string eventType = op[0] as string;
                JObject eventDescription = op[1] as JObject;
                JEnumerable<JToken> tokens = eventDescription.Children();
                UserHistoryElement newHistory = null;
                switch (eventType)
                {
                    case "account_create":
                        break;
                }
            }
            return result;
        }
    }
}
