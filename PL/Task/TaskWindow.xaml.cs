using BO;
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
        List<BO.TaskInList> dependenciesTasksList = new List<BO.TaskInList>();

        public ObservableCollection<BO.Task> CurrentTask
        {
            get { return (ObservableCollection<BO.Task>)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        public static readonly DependencyProperty TaskProperty =
        DependencyProperty.Register("CurrentTask", typeof(ObservableCollection<BO.Task>),
        typeof(TaskWindow), new PropertyMetadata(null));

        private void addDependency_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int dependency = int.Parse(dependencyTextBox.Text); // Get the text from the TextBox
                BO.Task? task = s_bl.task.Read(dependency);
                if (task == null)
                {
                    MessageBox.Show("There is no task with the given id");
                }
                else
                {

                    var taskInList = new TaskInList();
                    taskInList.Id = task.Id;
                    taskInList.Description = task.Description;
                    taskInList.Alias = task.Alias;
                    taskInList.Status = task.Status;
                    dependenciesTasksList.Add(taskInList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid task id: " + ex.Message);
            }
            dependencyTextBox.Text = "";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentTask != null && CurrentTask.Count > 0)
            {
                BO.Task task = CurrentTask[0];
                if (dependenciesTasksList != null)
                    task.DependenciesList = dependenciesTasksList!; // Assign the list of dependencies
                if (STATE == 0)
                {
                    try
                    {
                        s_bl.task.Create(task);
                        MessageBox.Show("Task created successfully");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to create this task: " + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        s_bl.task.Update(task);
                        MessageBox.Show("Task updated successfully");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to update this task: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("The details provided are not sufficient.");
            }
        }


        public TaskWindow(int id = 0)
        {
            InitializeComponent();

            var engineers = s_bl.Engineer.ReadAll(engineer => engineer.Task == null);

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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


    }
}
