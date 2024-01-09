using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        private int STATE;

        public ObservableCollection<BO.Engineer> CurrentEngineer
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }

        public static readonly DependencyProperty EngineerProperty =
        DependencyProperty.Register("CurrentEngineer", typeof(ObservableCollection<BO.Engineer>),
        typeof(EngineerWindow), new PropertyMetadata(null));

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentEngineer != null && CurrentEngineer.Count > 0)
            {
                BO.Engineer engineer = CurrentEngineer[0];

                if (STATE == 0)
                {
                    try
                    {
                        s_bl.Engineer.Create(engineer);
                        MessageBox.Show("engineer created seccessfuly");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("not able to create this engineer" + ex);
                    }
                }
                else
                {
                    try
                    {
                        s_bl.Engineer.Update(engineer);
                        MessageBox.Show("engineer updated seccessfully");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("not able to create this engineer" + ex);
                    }
                }
            }
            else
            {
                MessageBox.Show("the details that were given aren't good, or not enougth");
            }

        }

        public EngineerWindow(int id = 0)
        {
            InitializeComponent();
            BO.Engineer engineer;
            if (id == 0)
            {
                STATE = 0;
                engineer = new BO.Engineer();
            }
            else
            {
                STATE = 1;
                try
                {
                    engineer = s_bl.Engineer.Read(id);
                }
                catch (BO.BlDoesNotExistException ex)
                {
                    throw new Exception($"There is no engineer with id:{id}", ex);
                }
            }
            CurrentEngineer = new ObservableCollection<BO.Engineer> { engineer };
        }
    }
}
