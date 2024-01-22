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

namespace PL.Task;

/// <summary>
/// Interaction logic for TaskListWindow.xaml
/// </summary>
public partial class TaskListWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public BO.EngineerExperience TaskLevel { get; set; } = BO.EngineerExperience.None;

    public ObservableCollection<BO.Task> TaskList
    {
        get { return (ObservableCollection<BO.Task>)GetValue(TaskListProperty); }
        set { SetValue(TaskListProperty, value); }
    }
    public static readonly DependencyProperty TaskListProperty =
    DependencyProperty.Register("TaskList", typeof(ObservableCollection<BO.Task>),
    typeof(TaskListWindow), new PropertyMetadata(null));

    public TaskListWindow()
    {
        InitializeComponent();

        var temp = s_bl?.task.ReadAll();
        TaskList = temp == null ? new() : new(temp!);

    }

    private void cbTaskLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        if (comboBox != null && comboBox.SelectedItem != null)
        {
            TaskLevel = (BO.EngineerExperience)comboBox.SelectedItem;
            var temp = TaskLevel == BO.EngineerExperience.None ? s_bl?.task.ReadAll() :
                s_bl?.task.ReadAll(item => item.ComplexityLevel == TaskLevel);
            TaskList = (temp == null) ? new() : new(temp!);
        }
    }
}
