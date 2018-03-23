using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserNotes
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Page
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
            object id = null;

            string query = "select id from tblUser where username = " + "'" + txtUsername.Text + "'" + " and password = " + "'" + txtPassword.Text + "'";

            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                id = cmd.ExecuteScalar();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }

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
