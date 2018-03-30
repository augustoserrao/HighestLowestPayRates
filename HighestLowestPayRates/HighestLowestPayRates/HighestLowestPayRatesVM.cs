/* Augusto Serrao
   Deepti Aggarwal
   Hartaj Dhillon
   Gagandeep Singh
   Shoaib Hassan
*/

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HighestLowestPayRates
{
    class HighestLowestPayRatesVM : INotifyPropertyChanged
    {
        static HighestLowestPayRatesVM instance;
        EmployeeDB db;
        Employee employeeSelected = new Employee();

        ObservableCollection<Employee> employeeList;

        public ObservableCollection<Employee> EmployeeList
        {
            get { return employeeList; }
            set { employeeList = value; }
        }

        public Employee EmployeeSelected
        {
            get { return employeeSelected; }
            set { employeeSelected = value; }
        }

        private HighestLowestPayRatesVM()
        {
            // Create default employee list for test
            List<Employee> defaultEmployeeList = new List<Employee>();
            defaultEmployeeList.Add(new Employee() { ID = 0, FirstName = "Cristiano", LastName = "Ronaldo"  , Position = "Manager"                  , Age = 33, HourlyPayRate = 35.50m, ImagePath = "Images/Cristiano_Ronaldo.jpg" });
            defaultEmployeeList.Add(new Employee() { ID = 1, FirstName = "Kevin"    , LastName = "De Bruyne", Position = "Junior Software Developer", Age = 26, HourlyPayRate = 22    , ImagePath = "Images/De_Bruine.jpg"         });
            defaultEmployeeList.Add(new Employee() { ID = 2, FirstName = "Antoine"  , LastName = "Griezmann", Position = "Software Developer"       , Age = 27, HourlyPayRate = 29.30m, ImagePath = "Images/Griezmann.jpg"         });
            defaultEmployeeList.Add(new Employee() { ID = 3, FirstName = "Lionel"   , LastName = "Messi"    , Position = "Director"                 , Age = 30, HourlyPayRate = 45    , ImagePath = "Images/Messi.png"             });
            defaultEmployeeList.Add(new Employee() { ID = 4, FirstName = "Thomas"   , LastName = "Muller"   , Position = "Senior Software Developer", Age = 28, HourlyPayRate = 32.25m, ImagePath = "Images/Muller.jpg"            });
            defaultEmployeeList.Add(new Employee() { ID = 5, FirstName = "Willian"  , LastName = "Borges"   , Position = "Software Developer"       , Age = 29, HourlyPayRate = 30    , ImagePath = "Images/Willian.jpg"           });

            db = EmployeeDB.GetInstance();
            List<Employee> dbList = db.Get();

            // Check if DB contains default employees
            foreach (Employee e in defaultEmployeeList)
            {
                bool found = false;

                foreach (Employee dbe in dbList)
                {
                    if (dbe.ID == e.ID)
                        found = true;
                }

                if (!found)
                    db.Add(e);
            }

            employeeList = new ObservableCollection<Employee>(db.Get());
        }

        public static HighestLowestPayRatesVM GetInstance()
        {
            if (instance == null)
                instance = new HighestLowestPayRatesVM();
            return instance;
        }

        public void SelectEmployee(Employee employee)
        {
            employeeSelected = employee;
        }

        public void SelectHighestPaidEmployee()
        {
            Employee employee = EmployeeList.ElementAt(0);

            foreach (Employee e in EmployeeList)
            {
                if (e.HourlyPayRate > employee.HourlyPayRate)
                    employee = e;
            }

            employeeSelected = employee;
        }

        public void SelectLowestPaidEmployee()
        {
            Employee employee = EmployeeList.ElementAt(0);

            foreach (Employee e in EmployeeList)
            {
                if (e.HourlyPayRate < employee.HourlyPayRate)
                    employee = e;
            }

            employeeSelected = employee;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
