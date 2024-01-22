using PL.Engineer;
using PL.Task;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private void btnEngineers_Click(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }

        private void btnTasks_Click(object sender, RoutedEventArgs e)
        {
            new TaskListWindow().Show();
        }

        private void btnDataInitialization_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to initialize all the data?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // User clicked "Yes" button
                DalTest.Initialization.Do();
            }

        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
