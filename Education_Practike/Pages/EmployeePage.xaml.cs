using Education_Practike.DBConnection;
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

namespace Education_Practike.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        public static List<Employee> employees { get; set; }
        public static List<Department> departments { get; set; }
        public static Employee loggedUser;
        public EmployeePage()
        {
            InitializeComponent();
            UserNameTB.Text = DBConnection.DbConnection.loginedUser.Surname;
            InitializeDataInPage();
            loggedUser = DbConnection.loginedUser;
            this.DataContext = this;
        }
        private void InitializeDataInPage()
        {
            employees = new List<Employee>(DBConnection.DbConnection.Instityte.Employee.ToList());
            departments = new List<Department>(DBConnection.DbConnection.Instityte.Department.ToList());
        }

        private void BackBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

        private void Refresh()
        {
            EmployeeLV.ItemsSource = employees;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void AddEmployeeBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEmployeePage());
        }

        private void EditEmployeeBT_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeLV.SelectedItem is Employee employee)
            {
                NavigationService.Navigate(new InfoEmployeePage(employee));

            }
        }

        private void DeleteEmployeeBT_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeLV.SelectedItem is Employee employee)
            {
                DbConnection.Instityte.Employee.Remove(employee);
                DbConnection.Instityte.SaveChanges();
                Refresh();
            }
        }
    }

    
}
