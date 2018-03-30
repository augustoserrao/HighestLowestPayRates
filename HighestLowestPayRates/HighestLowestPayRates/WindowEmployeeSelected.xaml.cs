/* Augusto Serrao
   Deepti Aggarwal
   Hartaj Dhillon
   Gagandeep Singh
   Shoaib Hassan
*/

using System.Windows;

namespace HighestLowestPayRates
{
    /// <summary>
    /// Interaction logic for WindowEmployeeSelected.xaml
    /// </summary>
    public partial class WindowEmployeeSelected : Window
    {
        HighestLowestPayRatesVM vm = HighestLowestPayRatesVM.GetInstance();

        public WindowEmployeeSelected()
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
