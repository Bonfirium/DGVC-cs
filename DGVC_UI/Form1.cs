using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ditch;
using Ditch.Helpers;
using System.Configuration;

namespace DGVC_UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void sign_in_Click(object sender, EventArgs e)
        {
            log_in();

        }

        void log_in()
        {
            string username = textBoxLogin.Text;
            string password = textBoxPassword.Text;
            var Login = new Dictionary<string, string>() { { "Golos", username } };
            var UserPrivateKeys = new Dictionary<string, List<byte[]>>() {
                { "Golos", new List<byte[]> { Base58.TryGetBytes(password) } },
            };
            var Chain = new Dictionary<string, ChainInfo>();
            var golosChainInfo = ChainManager.GetChainInfo(KnownChains.Golos);
            Chain.Add("Golos", golosChainInfo);
            var Golos = new OperationManager(golosChainInfo.Url, golosChainInfo.ChainId);
            var accountCount = Golos.GetAccountCount();
            this.Close();
            //label1.Text = "Account Count = " + accountCount.Result;
            menu menu = new menu();
            //menu.R();
            
        }

    }
}
