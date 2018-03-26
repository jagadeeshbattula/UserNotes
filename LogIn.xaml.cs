using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UserNotes
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// 
    /// @author Jagadeesh Battula jagadeesh@goftx.com
    /// </summary>
    public partial class LogIn : Page
    {
        /*
         * Constructor for LogIn
         */
        public LogIn()
        {
            InitializeComponent();
        }

        /* 
         * btnSubmit_Click event
         * 
         * btnSbumit click event for checking log in credentials
         */
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            string query                    = "select id from tblUser where username = " + "'" + txtUsername.Text + "'" + " and password = " + "'" + txtPassword.Text + "'";
            object id                       = null;

            DataConnection dataConnection   = new DataConnection();
            id                              = dataConnection.Login(query);

            if (id != null)
            {
                UserHome userHome = new UserHome(id.ToString());
                this.NavigationService.Navigate(userHome, id.ToString());
            }
            else
            {
                MessageBox.Show("User name and/or Password do not match !");
            }
        }
    }
}
