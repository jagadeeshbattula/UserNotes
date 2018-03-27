using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace UserNotes
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// 
    /// @author Jagadeesh Battula jagadeesh@goftx.com
    /// </summary>
    public partial class LogIn : Page
    {
        //Entity Frame Work class
        WPFApplicationEntities _db  = new WPFApplicationEntities();
        //Hash key for Encoding password
        public static string key    = "usernoteapplication";

        /*
         * Constructor for LogIn
         */
        public LogIn()
        {
            InitializeComponent();

            btnLogin.Visibility             = Visibility.Hidden;
            lblTitle.Content                = "Log In Here";
            lblConformPassword.Visibility   = Visibility.Hidden;
            txtConformPassword.Visibility   = Visibility.Hidden;
            btnSignupSubmit.Visibility      = Visibility.Hidden;
        }

        /*
         * btnLogin_Click event
         * 
         * Enables log in controlles of Log in form and disable Signup controlles
         */
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            btnLogin.Visibility             = Visibility.Hidden;
            btnSignup.Visibility            = Visibility.Visible;

            lblTitle.Content                = "Log In Here";

            lblConformPassword.Visibility   = Visibility.Hidden;
            txtConformPassword.Visibility   = Visibility.Hidden;

            btnLoginSubmit.Visibility       = Visibility.Visible;
            btnSignupSubmit.Visibility      = Visibility.Hidden;

        }

        /*
         * btnSignup_Click event
         * 
         * Enables Sign up controlles of Log in form and disable Log in controlles
         */
        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            btnSignup.Visibility            = Visibility.Hidden;
            btnLogin.Visibility             = Visibility.Visible;

            lblTitle.Content                = "Sign Up Here";

            lblConformPassword.Visibility   = Visibility.Visible;
            txtConformPassword.Visibility   = Visibility.Visible;

            btnLoginSubmit.Visibility       = Visibility.Hidden;
            btnSignupSubmit.Visibility      = Visibility.Visible;
        }

        /* 
         * CheckForValidation function
         * 
         * For checking input feilds validation
         */
        public bool CheckForValidation()
        {
            bool allGood = false;

            if(txtUsername.Text == null || txtPassword.Password == null || txtUsername.Text.Trim().Length == 0 || txtPassword.Password.Trim().Length == 0)
            {
                allGood = false;
            }
            else
            {
                allGood = true;
            }

            return allGood;
        }

        /* 
         * btnLoginSubmit_Click event
         * 
         * Event for checking log in credentials
         */
        private void btnLoginSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(!CheckForValidation())
            {
                MessageBox.Show("User Name and Password feilds required..!");
            }
            else
            {
                string password = Encrypt(txtPassword.Password);

                int id = (from user in _db.tblUsers
                          where user.username == txtUsername.Text && user.password == password
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

        /* 
         * btnSignupSubmit_Click event
         * 
         * Event for creating new user in DataBase
         */
        private void btnSignupSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckForValidation() || txtConformPassword.Password == "" || txtConformPassword.Password.Trim().Length == 0)
            {
                MessageBox.Show("User Name, Password and Conform Password feilds required..!");
            }
            else
            {
                if(txtPassword.Password != txtConformPassword.Password )
                {
                    MessageBox.Show("Password and Conform Password feilds do not match");
                }
                else
                {
                    string password                 = Encrypt(txtPassword.Password);

                    tblUser user = new tblUser()
                    {
                        username = txtUsername.Text,
                        password = password
                    };

                    _db.tblUsers.Add(user);
                    _db.SaveChanges();

                    btnLogin.Visibility             = Visibility.Hidden;
                    btnSignup.Visibility            = Visibility.Visible;

                    lblTitle.Content                = "Log In Here";

                    lblConformPassword.Visibility   = Visibility.Hidden;
                    txtConformPassword.Visibility   = Visibility.Hidden;

                    btnLoginSubmit.Visibility       = Visibility.Visible;
                    btnSignupSubmit.Visibility      = Visibility.Hidden;
                }
            }
        }

        /* 
         * Encrypt function
         * 
         * Encrypts password with hash key
         */
        public string Encrypt(string password)
        {
            byte[] bytesBuff = Encoding.Unicode.GetBytes(password);

            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes crypto   = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                aes.Key                     = crypto.GetBytes(32);
                aes.IV                      = crypto.GetBytes(16);

                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cStream.Write(bytesBuff, 0, bytesBuff.Length);
                        cStream.Close();
                    }

                    password = System.Convert.ToBase64String(mStream.ToArray());
                }
            }
            return password;
        }
    }
}
