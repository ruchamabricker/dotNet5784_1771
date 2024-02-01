using PL.Engineer;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        private int STATE;
        public bool IsTextBoxEnabled { get; set; }

        public ObservableCollection<BO.Task> CurrentTask
        {
            get { return (ObservableCollection<BO.Task>)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        public static readonly DependencyProperty TaskProperty =
        DependencyProperty.Register("CurrentTask", typeof(ObservableCollection<BO.Task>),
        typeof(TaskWindow), new PropertyMetadata(null));

        public void addDependency_Click(object sender, RoutedEventArgs e)
        {
            IsTextBoxEnabled = !IsTextBoxEnabled;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentTask != null && CurrentTask.Count > 0)
            {
                BO.Task task = CurrentTask[0];

                if (STATE == 0)
                {
                    try
                    {
                        s_bl.task.Create(task);
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
                        s_bl.task.Update(task);
                        MessageBox.Show("task updated seccessfully");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("not able to create this task" + ex);
                    }
                }
            }
            else
            {
                MessageBox.Show("the details that were given aren't good, or not enougth");
            }

        }

        private void ToggleTextBox()
        {
            IsTextBoxEnabled = !IsTextBoxEnabled;
        }
        public TaskWindow(int id = 0)
        {
            InitializeComponent();

            IsTextBoxEnabled = false;

            var engineers = s_bl.Engineer.ReadAll(engineer=>engineer.Task==null);

            BO.Task task;
            if (id == 0)
            {
                STATE = 0;
                task = new BO.Task();
                task.Engineer = new BO.EngineerInTask();

            }
            else
            {
                STATE = 1;
                try
                {
                    task = s_bl.task.Read(id)!;
                }
                catch (BO.BlDoesNotExistException ex)
                {
                    throw new Exception($"There is no engineer with id:{id}", ex);
                }
            }
            CurrentTask = new ObservableCollection<BO.Task> { task };

        }
    }
}
