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
using Education_Practike.DBConnection;

namespace Education_Practike.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExamPage.xaml
    /// </summary>
    public partial class ExamPage : Page
    {
        public static List<Exam> exams { get; set; }
        public static List<Teacher> teachers { get; set; }
        public static List<Employee> employees { get; set; }
        public static List<Disciplina> disciplinas { get; set; }
        public static List<Student> students { get; set; }
        public static Employee loggedUser;
        public ExamPage()
        {
            InitializeComponent();
            UserNameTB.Text = DBConnection.DbConnection.loginedUser.Surname;
            exams = new List<Exam>(DBConnection.DbConnection.Instityte.Exam.ToList());
            teachers = new List<Teacher>(DBConnection.DbConnection.Instityte.Teacher.ToList());
            employees = new List<Employee>(DBConnection.DbConnection.Instityte.Employee.ToList());
            disciplinas = new List<Disciplina>(DBConnection.DbConnection.Instityte.Disciplina.ToList());
            students = new List<Student>(DBConnection.DbConnection.Instityte.Student.ToList());
            loggedUser = DbConnection.loginedUser;
            this.DataContext = this;

        }

        private void BackBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

       

        private void ExamLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ExamLV.SelectedItem is Exam exam)
            {
                ExamLV.SelectedItem = null;
                NavigationService.Navigate(new ListStudentsPage(exam));
            }
            
        }

        private void Refresh()
        {
            ExamLV.ItemsSource = exams;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
