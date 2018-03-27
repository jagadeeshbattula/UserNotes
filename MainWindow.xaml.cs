using System.Windows;

namespace UserNotes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// @author Jagadeesh Battula jagadeesh@goftx.com
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
         * Constructor for MainWindow
         */
        public MainWindow()
        {
            InitializeComponent();

            mainFrame.Content = new LogIn();
        }
    }
}
