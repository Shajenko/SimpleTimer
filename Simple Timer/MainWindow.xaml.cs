using System.Windows;
using System.ComponentModel; // CancelEventArgs


namespace Simple_Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            System.Environment.Exit(0);
            // Kill all threads
        }
    }
}
