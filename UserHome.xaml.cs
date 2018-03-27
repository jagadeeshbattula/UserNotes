using System;
using System.Data;
using System.Linq;
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
        //Entity Frame Work class
        WPFApplicationEntities _db = new WPFApplicationEntities();
        
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
        public UserHome(int id)
        {
            InitializeComponent();

            if(id != 0)
            {
                txtUserId.Text = id.ToString();
                txtViewNote.Visibility = Visibility.Hidden;
                ViewUserNotes(id);
            }
        }

        /* 
         * ViewUserNotes function
         * 
         * Get all notes of loggedin user
         */
        private void ViewUserNotes(int id)
        {
            btnSubmitUpdateNote.IsEnabled   = false;
            btnSubmitSaveNote.IsEnabled     = true;

            gridUserNotes.ItemsSource       = _db.tblNotes.Where(n => n.user_id == id).ToList();

        }

        /* 
         * deleteBtn_Click event
         * 
         * Delete the selected note from DataBase
         */
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            int userId      = int.Parse(txtUserId.Text);
            int id          = (gridUserNotes.SelectedItem as tblNote).id;
            var deleteNote  = _db.tblNotes.Where(m => m.id == id).Single();

            _db.tblNotes.Remove(deleteNote);
            _db.SaveChanges();

            gridUserNotes.ItemsSource = _db.tblNotes.Where(n => n.user_id == userId).ToList();
        }

        /* 
         * updateBtn_Click event
         * 
         * Populate the selected note to TextBox txtNewNote for updating
         */
        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            txtNewNote.Text                 = (gridUserNotes.SelectedItem as tblNote).note;
            btnSubmitUpdateNote.IsEnabled   = true;
            btnSubmitSaveNote.IsEnabled     = false;
        }

        /* 
         * btnSubmitUpdateNote_Click event
         * 
         * Update the note and save it to DataBase
         */
        private void btnSubmitUpdateNote_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewNote.Text.Trim().Length == 0 || txtNewNote.Text == "")
            {
                MessageBox.Show("Please enter note before updating !");
            }
            else
            {
                int userId          = int.Parse(txtUserId.Text);
                int id              = (gridUserNotes.SelectedItem as tblNote).id;

                tblNote updateNote  = (from n in _db.tblNotes where n.id == id select n).Single();
                updateNote.user_id  = userId;
                updateNote.note     = txtNewNote.Text;

                _db.SaveChanges();

                gridUserNotes.ItemsSource       = _db.tblNotes.Where(n => n.user_id == userId).ToList();
                btnSubmitUpdateNote.IsEnabled   = false;
                btnSubmitSaveNote.IsEnabled     = true;
                txtNewNote.Text                 = "";
            }
        }

        /* 
        * btnSubmitSaveNote_Click event
        * 
        * Save new note to database
        */
        private void btnSubmitSaveNote_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewNote.Text.Trim().Length == 0 || txtNewNote.Text == "")
            {
                MessageBox.Show("Please enter note before saving !");
            }
            else
            {
                tblNote newNote = new tblNote()
                {
                    user_id     = int.Parse(txtUserId.Text),
                    note        = txtNewNote.Text,
                    created_at  = DateTime.Now
                };

                _db.tblNotes.Add(newNote);
                _db.SaveChanges();

                int id                          = int.Parse(txtUserId.Text);
                gridUserNotes.ItemsSource       = _db.tblNotes.Where(n => n.user_id == id).ToList();
                btnSubmitUpdateNote.IsEnabled   = false;
                btnSubmitSaveNote.IsEnabled     = true;
                txtNewNote.Text                 = "";
            }
        }

        /* 
        * btnLogOut_Click event
        * 
        * Return back to log in page
        */
        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            LogIn logIn = new LogIn();

            this.NavigationService.Navigate(logIn);
        }

        /* 
        * gridUserNotes_MouseDoubleClick event
        * 
        * Populates longer notes body into txtViewNote TextBox for accessibility
        */
        private void gridUserNotes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int lengthOfNote = ((gridUserNotes.SelectedItem as tblNote).note).Length;

            if (lengthOfNote >= 80)
            {
                txtViewNote.Visibility  = Visibility.Visible;
                txtViewNote.Text        = (gridUserNotes.SelectedItem as tblNote).note;
            }
            else
            {
                txtViewNote.Visibility = Visibility.Hidden;
            }
        }
    }
}