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

namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerListWindow.xaml
/// </summary>
public partial class EngineerListWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    private BO.EngineerExperience EngineerLevel { get; set; } = BO.EngineerExperience.None;

    public ObservableCollection<BO.Engineer> EngineerList
    {
        get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerListProperty); }
        set { SetValue(EngineerListProperty, value); }
    }
    public static readonly DependencyProperty EngineerListProperty =
    DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.Engineer>),
    typeof(EngineerListWindow), new PropertyMetadata(null));

    private void cbEngineerExperience_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        if (comboBox != null && comboBox.SelectedItem != null)
        {
            EngineerLevel = (BO.EngineerExperience)comboBox.SelectedItem;
            var temp = EngineerLevel == BO.EngineerExperience.None ? s_bl?.Engineer.ReadAll() :
                s_bl?.Engineer.ReadAll(item => item.Level == EngineerLevel);
            EngineerList = (temp == null) ? new() : new(temp!);
        }
    }

    public EngineerListWindow()
    {
        InitializeComponent();
        var temp = s_bl?.Engineer.ReadAll();
        EngineerList = temp == null ? new() : new(temp!);

        Activated += EngineerListWindow_Activated!;
    }
    private void EngineerListWindow_Activated(object sender, EventArgs e)
    {
        // Execute the query and update the list
        var temp = EngineerLevel == BO.EngineerExperience.None ? s_bl?.Engineer.ReadAll() :
            s_bl?.Engineer.ReadAll(item => item.Level == EngineerLevel);
        EngineerList = (temp == null) ? new() : new(temp!);
    }

    private void engineerClicked_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.Engineer? engineerInList = (sender as ListView)?.SelectedItem as BO.Engineer;
        if (engineerInList != null)
            new EngineerWindow(engineerInList.Id).ShowDialog();
    }

    private void handleNewEngineer_Click(object sender, RoutedEventArgs e)
    {
        new EngineerWindow().ShowDialog();
    }
}
