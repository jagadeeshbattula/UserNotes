using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotes
{
    /*
     *DataConnection class for connecting to DataBase and Query execution
     * 
     * @author Jagadeesh Battula jagadeesh@goftx.com
     */
    class DataConnection
    {
        /* 
         * AllNotes
         * 
         * Get all notes of logged in user from database 
         */
        public DataTable AllNotes(string query)
        {
            SqlConnection sqlConnection = new SqlConnection
            {
                ConnectionString        = ConfigurationManager.ConnectionStrings["connect"].ConnectionString
            };

            DataTable dt = new DataTable();

            try
            {
                sqlConnection.Open();

                SqlDataAdapter da   = new SqlDataAdapter();
                SqlCommand cmd      = new SqlCommand(query, sqlConnection);
                da.SelectCommand    = cmd;

                da.Fill(dt);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }

            return dt;
        }

        /* 
         * AddNoteToDB
         * 
         * Add notes of logged in user to DataBase
         */
        public int AddNoteToDB(string query)
        {
            SqlConnection sqlConnection = new SqlConnection
            {
                ConnectionString        = ConfigurationManager.ConnectionStrings["connect"].ConnectionString
            };

            int Inserted = 0;

            try
            {
                sqlConnection.Open();

                SqlCommand cmd  = new SqlCommand(query, sqlConnection);
                Inserted        = cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }

            return Inserted;
        }

        /* 
         * Login
         * 
         * Log in user, checking log in credentials
         */
        public object Login(string query)
        {

            SqlConnection sqlConnection = new SqlConnection
            {
                ConnectionString        = ConfigurationManager.ConnectionStrings["connect"].ConnectionString
            };

            object id = null;

            try
            {
                sqlConnection.Open();

                SqlCommand cmd  = new SqlCommand(query, sqlConnection);
                id              = cmd.ExecuteScalar();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }

            return id;
        }
    }
}
