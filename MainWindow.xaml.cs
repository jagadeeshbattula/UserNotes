using System;
using System.Collections.Generic;
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
