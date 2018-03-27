using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace UserNotes
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// 
    /// @author Jagadeesh Battula jagadeesh@goftx.com
    /// </summary>
    public partial class LogIn : Page
    {
        WPFApplicationEntities _db = new WPFApplicationEntities();
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

            int id = (from user in _db.tblUsers
                      where user.username == txtUsername.Text && user.password == txtPassword.Text
                      select user.id).SingleOrDefault();
            
            if (id != 0)
            {
                UserHome userHome = new UserHome(id);
                this.NavigationService.Navigate(userHome, id);
            }
            else
            {
                MessageBox.Show("User name and/or Password do not match !");
            }
        }
    }
}
