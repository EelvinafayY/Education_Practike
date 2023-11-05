using Education_Practike.DBConnection;
using Education_Practike.Function;
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

namespace Education_Practike.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public static List<Employee> employees { get; set; }
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void GostBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListDisciplinaPage());
        }

        private void VxodBT_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTB.Text.Trim();
            string password = PasswordPB.Password.Trim();


            employees = new List<Employee>(DBConnection.DbConnection.Instityte.Employee.ToList());
            Employee currentUser = AuthorizationFunc.AuthorizationEmpl(login, password);
            DbConnection.loginedUser = currentUser;
            if (currentUser != null)
            {
                if (currentUser.Post == "преподаватель")
                    NavigationService.Navigate(new ExamPage());
                else if (currentUser.Post == "зав. кафедрой")
                    NavigationService.Navigate(new DepartmentPage());
                else if (currentUser.Post == "инженер")
                    NavigationService.Navigate(new EmployeePage());


            }
            else
            {
                MessageBox.Show("Такого пользователя не существует(((");
            }
        }
    }
}
