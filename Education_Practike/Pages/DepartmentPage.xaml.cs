using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Education_Practike.DBConnection;

namespace Education_Practike.Pages
{
    /// <summary>
    /// Логика взаимодействия для DepartmentPage.xaml
    /// </summary>
    public partial class DepartmentPage : Page
    {
        public static List<Employee> employees { get; set; }
        public static List<Department> departments { get; set; }
        public static List<Faculty> faculties { get; set; }
        public static Employee loggedUser;
        public static Department department1 = new Department();
        public DepartmentPage()
        {
            InitializeComponent();
            loggedUser = DbConnection.loginedUser;
            employees = new List<Employee>(DbConnection.Instityte.Employee.ToList());
            departments = new List<Department>(DbConnection.Instityte.Department.Where(x => x.Code == DbConnection.loginedUser.Code).ToList());
            faculties = new List<Faculty> (DbConnection.Instityte.Faculty.ToList());
            this.DataContext = this;
            FacultyCB.ItemsSource = DbConnection.Instityte.Faculty.ToList();

        }

        private void DepartmentsLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepartmentsLV.SelectedItem is Department department)
            {
              DepartmentsLV.SelectedItem = null;
              NavigationService.Navigate(new AddListDisciplinaPage(department));
            }
        }

       
        private void Refresh()
        {
            DepartmentsLV.ItemsSource = departments;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void BackBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

        private void SaveBT_Click(object sender, RoutedEventArgs e)
        {
            
                var awds = "Наименование: " + NameTB.Text
                + " " + "Код: " + CodeTB.Text;


                if (MessageBox.Show(awds, "Проверьте корректность введенных данных", MessageBoxButton.YesNo)
                    == MessageBoxResult.Yes)
                {

                    department1.Name = NameTB.Text;
                    department1.Code = CodeTB.Text;
                    var t = FacultyCB.SelectedItem as Faculty;
                    department1.faculty = t.Abbreviation;

                    
                    DbConnection.Instityte.Department.Add(department1);
                    DbConnection.Instityte.SaveChanges();

                }
            }
    }
}
