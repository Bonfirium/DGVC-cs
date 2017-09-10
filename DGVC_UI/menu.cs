using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using TestProject;

namespace DGVC_UI
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
            textBox1.Text += Directory.GetCurrentDirectory();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Class1.im(sender.ToString().Substring(35));
            textBox1.Text += Environment.NewLine;
            textBox1.Text += Directory.GetCurrentDirectory()+">";
        }


    }
}
