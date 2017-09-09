using System.Windows.Forms;
using TestProject;

namespace DGVC_UI
{
    public partial class FormWelcome : Form
    {
        public FormWelcome()
        {
            InitializeComponent();
            label_UsersCount.Text = "Зарегестрированно пользователей: " + GolosManager.GetUsersCount();
        }
    }
}
