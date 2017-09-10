using Ditch;
using Ditch.Helpers;
using Ditch.JsonRpc;
using Ditch.Operations.Get;
using Ditch.Operations.Post;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace TestProject {
    public class GolosManager {
        public OperationManager Golos { get; protected set; }
        public Dictionary<string, ChainInfo> Chain;
        public string URL { get; internal protected set; }
        public bool Logged { get; internal protected set; }
        public string UserName { get; internal protected set; }
        private List<byte[]> _authList;

        public GolosManager( ) {
            Chain = new Dictionary<string, ChainInfo>( );
            var golosChainInfo = ChainManager.GetChainInfo(KnownChains.Golos);
            Chain.Add("Golos", golosChainInfo);
            URL = golosChainInfo.Url;
            Golos = new OperationManager(URL, golosChainInfo.ChainId);
        }

        public void Login(string username, string password) {
            UserName = username;
            _authList = new List<byte[]> { Base58.TryGetBytes(password) };
        }

        public ulong GetUsersCount( ) {
            return Golos.GetAccountCount( ).Result;
        }

        public void CreatePost(string title, string[] message, string[] tags) {
            var op = new PostOperation(URL, UserName, "Test DGVC", "It's just a test. Ignore it please.\nPeace! ^-^", "{\"app\": \"" + URL + " / 0.0.4\", \"tags\": [test]}");
            var popt = new BeneficiariesOperation(UserName, op.Permlink, Chain["Golos"].SbdSymbol, new Beneficiary(URL, 1000));
            var prop = Golos.VerifyAuthority(_authList, op);
            //Golos.BroadcastOperations(_authList, op, popt);
        }

        private JsonRpcResponse<KeyValuePair<uint, AppliedOperation>[]> GetUserHistoryAsJson(string userName, ulong from, uint limit) {
            return Golos.GetAccountHistory(userName, from, limit);
        }

        public List<UserHistoryElement> GetUserHistory(string userName, ulong from, uint limit) {
            JsonRpcResponse<KeyValuePair<uint, AppliedOperation>[]> json =
                GetUserHistoryAsJson(userName, from, limit);
            List<UserHistoryElement> result = new List<UserHistoryElement>( );
            KeyValuePair<uint, AppliedOperation>[] keyValuePair =
                json.Result as KeyValuePair<uint, AppliedOperation>[];
            foreach (KeyValuePair<uint, AppliedOperation> historyElement in keyValuePair) {
                AppliedOperation operation = historyElement.Value;
                uint blockId = operation.Block;
                object[] op = operation.Op;
                string eventType = op[0] as string;
                JObject eventDescription = op[1] as JObject;
                JEnumerable<JToken> tokens = eventDescription.Children( );
                UserHistoryElement newHistory = null;
                switch (eventType) {
                    case "account_create":
                        break;
                }
            }
            return result;
        }
    }
}
