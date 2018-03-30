/* Augusto Serrao
   Deepti Aggarwal
   Hartaj Dhillon
   Gagandeep Singh
   Shoaib Hassan
*/

using System;
using System.Windows;
using System.Windows.Controls;

namespace HighestLowestPayRates
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HighestLowestPayRatesVM vm = HighestLowestPayRatesVM.GetInstance();
        WindowEmployeeSelected employeeHighestWindow = null;
        WindowEmployeeSelected employeeLowestWindow = null;
        WindowEmployeeSelected employeeSelectedWindow = null;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void Employee_Selected(object sender, SelectionChangedEventArgs e)
        {
            vm.SelectEmployee((Employee)((ListBox)sender).SelectedItem);
            btnView.IsEnabled = true;
        }

        private void Button_Highest_Clicked(object sender, RoutedEventArgs e)
        {
            vm.SelectHighestPaidEmployee();

            if (employeeHighestWindow == null)
            {
                employeeHighestWindow = new WindowEmployeeSelected();
                employeeHighestWindow.Closed += employeeHighestWindowClosed;
                employeeHighestWindow.Show();
            }
        }

        private void employeeHighestWindowClosed(object sender, EventArgs e)
        {
            employeeHighestWindow = null;
        }

        private void Button_Lowest_Clicked(object sender, RoutedEventArgs e)
        {
            vm.SelectLowestPaidEmployee();

            if (employeeLowestWindow == null)
            {
                employeeLowestWindow = new WindowEmployeeSelected();
                employeeLowestWindow.Closed += employeeLowestWindowClosed;
                employeeLowestWindow.Show();
            }
        }

        private void employeeLowestWindowClosed(object sender, EventArgs e)
        {
            employeeLowestWindow = null;
        }

        private void Button_View_Clicked(object sender, RoutedEventArgs e)
        {
            if (employeeSelectedWindow == null)
            {
                employeeSelectedWindow = new WindowEmployeeSelected();
                employeeSelectedWindow.Closed += employeeSelectedWindowClosed;
                employeeSelectedWindow.Show();
            }
        }

        private void employeeSelectedWindowClosed(object sender, EventArgs e)
        {
            employeeSelectedWindow = null;
        }
    }
}
