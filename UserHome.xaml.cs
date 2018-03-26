using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace UserNotes
{
    /// <summary>
    /// Interaction logic for UserHome.xaml
    /// 
    /// @author Jagadeesh Battula jagadeesh@goftx.com
    /// </summary>
    public partial class UserHome : Page
    {
        /* 
         * Constructor for UserHome
         */
        public UserHome()
        {
            InitializeComponent();
        }

        /* 
         * Constructor with paramenters 
         */
        public UserHome(string id)
        {
            InitializeComponent();

            if(id != null)
            {
                txtUserId.Text = id;
                ViewUserNotes(id);
            }

        }

        /* 
         * ViewUserNotes function
         * 
         * Get all notes of loggedin user
         */
        private void ViewUserNotes(string id)
        {

            string query                    = "select * from tblNotes where user_id = " + "'" + id + "'";
            DataTable dt                    = new DataTable();

            DataConnection dataConnection   = new DataConnection();
            dt                              = dataConnection.AllNotes(query);

            gridUserNotes.ItemsSource       = dt.DefaultView;
        }

        /* 
         * btnSaveNote_Click event
         * 
         * SaveNote button click function to save new note to database
         */
        private void btnSaveNote_Click(object sender, RoutedEventArgs e)
        {
           
            int Inserted                    = 0;

            string query                    = "insert into tblNotes(user_id, note) values (" + (txtUserId.Text).ToString() + ", '" + txtNewNote.Text.ToString() + "')";

            DataConnection dataConnection   = new DataConnection();
            Inserted                        = dataConnection.AddNoteToDB(query);

            if (Inserted != 0)
            {
                UserHome userHome = new UserHome((txtUserId.Text).ToString());
                this.NavigationService.Navigate(userHome, (txtUserId.Text).ToString());
            }
        }

    }
}