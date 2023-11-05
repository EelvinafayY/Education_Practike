using Education_Practike.DBConnection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace Education_Practike.Pages
{
    /// <summary>
    /// Логика взаимодействия для InfoEmployeePage.xaml
    /// </summary>
    public partial class InfoEmployeePage : Page
    {
        public static List<Employee> employees = new List<Employee>();
        public static List<Department> departments = new List<Department>();
        Employee contextEmployee;
        public static Employee emp { get; set; }


        private void InitializeDataInPage()
        {
            DepartmentCB.ItemsSource = DbConnection.Instityte.Department.ToList();
            employees = new List<Employee>(DbConnection.Instityte.Employee.Where(x => x.Tab_Number == contextEmployee.Tab_Number).ToList());
            departments = new List<Department>(DbConnection.Instityte.Department.ToList());
            PostCB.ItemsSource = DbConnection.Instityte.Employee.Select(x => x.Post).Distinct().ToList();


        }
        public InfoEmployeePage(Employee employee)
        {
            InitializeComponent();
            contextEmployee = employee;
            emp = employee;
            InitializeDataInPage();
            this.DataContext = this;
        }

        private void BackBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeePage());
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
            var error = string.Empty;
            var validationContext = new ValidationContext(contextEmployee);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            if (!Validator.TryValidateObject(contextEmployee, validationContext, results, true))
            {
                foreach (var result in results)
                {
                    error += $"{result.ErrorMessage}\n";
                }
            }
            if (!string.IsNullOrWhiteSpace(error))
            {
                MessageBox.Show(error);
                return;
            }
            if (contextEmployee.Tab_Number == 0)
                DbConnection.Instityte.Employee.Add(contextEmployee);

            DbConnection.Instityte.SaveChanges();
            NavigationService.GoBack();
        }

        
    }
}
