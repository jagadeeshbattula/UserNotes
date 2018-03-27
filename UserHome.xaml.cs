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
            btnSubmitUpdateNote.IsEnabled = false;
            btnSubmitSaveNote.IsEnabled = true;
            gridUserNotes.ItemsSource = _db.tblNotes.Where(n => n.user_id == id).ToList();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            int userId = int.Parse(txtUserId.Text);
            int id = (gridUserNotes.SelectedItem as tblNote).id;
            var deleteNote = _db.tblNotes.Where(m => m.id == id).Single();
            _db.tblNotes.Remove(deleteNote);
            _db.SaveChanges();
            gridUserNotes.ItemsSource = _db.tblNotes.Where(n => n.user_id == userId).ToList();
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            txtNewNote.Text = (gridUserNotes.SelectedItem as tblNote).note;
            btnSubmitUpdateNote.IsEnabled = true;
            btnSubmitSaveNote.IsEnabled = false;
        }

        private void btnSubmitUpdateNote_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewNote.Text == " " || txtNewNote.Text == "")
            {
                MessageBox.Show("Please enter note before saving !");
            }
            else
            {
                int userId = int.Parse(txtUserId.Text);

                int id = (gridUserNotes.SelectedItem as tblNote).id;

                tblNote updateNote = (from n in _db.tblNotes where n.id == id select n).Single();
                updateNote.user_id = userId;
                updateNote.note = txtNewNote.Text;

                _db.SaveChanges();
                gridUserNotes.ItemsSource = _db.tblNotes.Where(n => n.user_id == userId).ToList();
                btnSubmitUpdateNote.IsEnabled = false;
                btnSubmitSaveNote.IsEnabled = true;
                txtNewNote.Text = "";
            }
            
        }

        /* 
        * btnSaveNote_Click event
        * 
        * SaveNote button click function to save new note to database
        */
        private void btnSubmitSaveNote_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewNote.Text == " " || txtNewNote.Text == "")
            {
                MessageBox.Show("Please enter note before saving !");
            }
            else
            {
                tblNote newNote = new tblNote()
                {
                    user_id = int.Parse(txtUserId.Text),
                    note = txtNewNote.Text,
                    created_at = DateTime.Now
                };

                _db.tblNotes.Add(newNote);
                _db.SaveChanges();

                int id = int.Parse(txtUserId.Text);
                gridUserNotes.ItemsSource = _db.tblNotes.Where(n => n.user_id == id).ToList();
                btnSubmitUpdateNote.IsEnabled = false;
                btnSubmitSaveNote.IsEnabled = true;
                txtNewNote.Text = "";
            }
        }
    }
}