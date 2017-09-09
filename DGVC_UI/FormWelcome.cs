using System.Windows.Forms;
using TestProject;
using System.Threading;
using System.Threading.Tasks;

namespace DGVC_UI
{
    public partial class FormWelcome : Form
    {
        public FormWelcome()
        {
            InitializeComponent();
            Label[] labels = new Label[] { label_UsersCount, label1, label2, label3, label4, label5, label6, label7, label8, label9 };
            foreach (var item in labels)
            {
                DisplayResultAsync(item);
            }
        }

        static async void DisplayResultAsync(Label label)
        {
            label.Text = "Зарегистрированно пользователей: " + await AsyncUsersCount();
        }

        static Task<ulong> AsyncUsersCount()
        {
            return Task.Run(() =>
            {
                return Program.GolosManager.GetUsersCount();
            });
        }
    }
}
