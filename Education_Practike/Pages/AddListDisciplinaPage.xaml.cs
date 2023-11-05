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
    /// Логика взаимодействия для AddListDisciplinaPage.xaml
    /// </summary>
    public partial class AddListDisciplinaPage : Page
    {
        public static List<Department> departments { get; set; }
        public static List<Disciplina> disciplines { get; set; }
        
        Department contextDepartment;
        public static Department dep { get; set; }
        public static Disciplina disciplina1 = new Disciplina();
        public AddListDisciplinaPage(Department department)
        {
            InitializeComponent();
            contextDepartment = department;
            dep = department;
            InitializeDataInPage();
            this.DataContext = this;
        }

       
        private void CatRaLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BackBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DepartmentPage());
        }

        private void InitializeDataInPage()
        {
            
            departments = new List<Department>(DbConnection.Instityte.Department.ToList());
            disciplines = new List<Disciplina>(DbConnection.Instityte.Disciplina.Where(x => x.Department.Code == contextDepartment.Code).ToList());
            

        }

        private void Refresh()
        {
            DisciplinaLV.ItemsSource = disciplines;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        

        private void SaveBT_Click(object sender, RoutedEventArgs e)
        {

            disciplina1.Name = NameTB.Text;
            disciplina1.Count_Hours = int.Parse(Count_HoursTB.Text);
            var t = contextDepartment;
            disciplina1.Department_Kod = t.Code;


            DbConnection.Instityte.Disciplina.Add(disciplina1);
            DbConnection.Instityte.SaveChanges();
            InitializeDataInPage();
        }

        private void DeleteBT_Click(object sender, RoutedEventArgs e)
        {
            if (DisciplinaLV.SelectedItem is Disciplina disciplina)
            {
                DbConnection.Instityte.Disciplina.Remove(disciplina);
                DbConnection.Instityte.SaveChanges();
                InitializeDataInPage();
            }
            
        }

        private void EditBT_Click(object sender, RoutedEventArgs e)
        {
            if (DisciplinaLV.SelectedItem is Disciplina disciplina)
            {
                DisciplinaLV.SelectedItem = null;
                NavigationService.Navigate(new EditDisciplinaPage(disciplina));
            }
        }
    }
}
