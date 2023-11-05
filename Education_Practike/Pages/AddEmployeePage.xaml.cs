using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using Microsoft.Win32;

namespace Education_Practike.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeePage.xaml
    /// </summary>
    public partial class AddEmployeePage : Page
    {
        public static List<Employee> employees = new List<Employee>();
        public static Employee emp = new Employee();
        public AddEmployeePage()
        {
            InitializeComponent();
            employees = new List<Employee>(DbConnection.Instityte.Employee.ToList());
            this.DataContext = this;
            DepartmentCB.ItemsSource = DbConnection.Instityte.Department.ToList();
        }

        private void AddPhotoBT_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };

            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                emp.Photo = File.ReadAllBytes(openFileDialog.FileName);
                PhotoIM.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void SaveBT_Click(object sender, RoutedEventArgs e)
        {
            var awds = "ФИО: " + FIOTB.Text
                + " " + "Зарплата: " + SalaryTB.Text;


            if (MessageBox.Show(awds, "Проверьте корректность введенных данных", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {

                emp.Surname = FIOTB.Text;
                emp.Salary = decimal.Parse(SalaryTB.Text);
                var t = PostCB.SelectedItem as TextBlock;
                emp.Post = PostCB.Text;
                emp.Login = LoginTB.Text;
                emp.Password = PasswordTB.Text;
                var a = DepartmentCB.SelectedItem as Department;
                emp.Code = a.Code;
                DbConnection.Instityte.Employee.Add(emp);
                DbConnection.Instityte.SaveChanges();

            }


        }
        private void BackBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeePage());
        }
    }
}
