﻿using System;
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
    /// Interaction logic for UserHome.xaml
    /// </summary>
    public partial class UserHome : Page
    {
        public UserHome()
        {
            InitializeComponent();
        }

        public UserHome(string id)
        {
            InitializeComponent();

            if(id != null)
            {
                txtUserId.Text = id;

                ViewUserNotes(id);

                
            }

        }

        

        private void ViewUserNotes(string id)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
            DataTable dt = new DataTable();

            string query = "select * from tblNotes where user_id = " + "'" + id + "'";

            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                da.SelectCommand = cmd;
                da.Fill(dt);

                gridUserNotes.ItemsSource = dt.DefaultView;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btnSaveNote_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
            int Inserted = 0;

            string query = "insert into tblNotes(user_id, note) values (" + (txtUserId.Text).ToString() + ", '" + txtNewNote.Text.ToString() + "')";

            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                Inserted = cmd.ExecuteNonQuery();

                if(Inserted != 0)
                {
                    UserHome userHome = new UserHome((txtUserId.Text).ToString());

                    this.NavigationService.Navigate(userHome, (txtUserId.Text).ToString());
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        
    }
}