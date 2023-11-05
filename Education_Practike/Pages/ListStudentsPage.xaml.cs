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
    /// Логика взаимодействия для ListStudentsPage.xaml
    /// </summary>
    public partial class ListStudentsPage : Page
    {
        public static List<Student> students { get; set; }
        public static List<Disciplina> disciplines { get; set; }
        public static List<Exam> exams { get; set; }
        Exam contextExam;
        public static Exam ex { get; set; }

        public ListStudentsPage(Exam exam)
        {
            InitializeComponent();
            contextExam = exam;
            ex = exam;
            InitializeDataInPage();
            this.DataContext = this;
        }

        private void InitializeDataInPage()
        {
            CBStudents.ItemsSource = DbConnection.Instityte.Student.ToList();
            students = new List<Student>(DbConnection.Instityte.Student).ToList();
            disciplines = new List<Disciplina>(DbConnection.Instityte.Disciplina.ToList());
            exams = new List<Exam>(DbConnection.Instityte.Exam.Where(x => x.Date == contextExam.Date && x.Disciplina.Kod_Disciplina == contextExam.Kod_Disciplina).ToList());
            
        }

        private void AddStudentBT_Click(object sender, RoutedEventArgs e)
        {
            string mark = "2 ";
            var TBmark = CBMark.SelectedValue as TextBlock;
            if (TBmark != null)
                mark = TBmark.Text;
            if (CBStudents.SelectedItem is Student student)
            {
                var exam = contextExam;
                exam.Student = student;
                exam.estimation = int.Parse(mark);
                var studentInList = exams.FirstOrDefault(x => x.Reg_Number == student.Reg_Number);
                if (studentInList != null)
                {
                    MessageBox.Show("Такой студент уже есть в экзамене");
                    return;
                }
                DbConnection.Instityte.Exam.Add(exam);
                DbConnection.Instityte.SaveChanges();
                Refresh();
                InitializeDataInPage();
            }

            
        }
        private void Refresh()
        {
            StudentsLV.ItemsSource = exams;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void BackBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void DeleteStudentBT_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsLV.SelectedItem is Exam exam)
            {
                DbConnection.Instityte.Exam.Remove(exam);
                DbConnection.Instityte.SaveChanges();
                InitializeDataInPage();
            }
        }
    } 
}
