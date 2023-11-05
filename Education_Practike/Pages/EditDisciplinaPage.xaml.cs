using Education_Practike.DBConnection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Логика взаимодействия для EditDisciplinaPage.xaml
    /// </summary>
    public partial class EditDisciplinaPage : Page
    {

        public static Disciplina disc {  get; set; }
        public static List<Disciplina> disciplinas = new List<Disciplina>();
        public static List<Department> departments = new List<Department>();
        Disciplina contextDisciplina;

        private void InitializeDataInPage()
        {
            DepartmentCB.ItemsSource = DbConnection.Instityte.Department.ToList();
            disciplinas = new List<Disciplina>(DbConnection.Instityte.Disciplina.Where(x => x.Kod_Disciplina == contextDisciplina.Kod_Disciplina).ToList());
            departments = new List<Department>(DbConnection.Instityte.Department.ToList());


        }
        
        public EditDisciplinaPage(Disciplina disciplina)
        {
            InitializeComponent();
            contextDisciplina = disciplina;
            disc = disciplina;
            InitializeDataInPage();
            this.DataContext = this;
        }

        private void BackBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveBT_Click(object sender, RoutedEventArgs e)
        {
            var error = string.Empty;
            var validationContext = new ValidationContext(contextDisciplina);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var disciplina = DBConnection.DbConnection.Instityte.Disciplina.FirstOrDefault(x => x.Kod_Disciplina == contextDisciplina.Kod_Disciplina);
            if (disciplina != null && disciplina != contextDisciplina)
                error += "This department shifr already exists";
            if (!Validator.TryValidateObject(contextDisciplina, validationContext, results, true))
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
            if (disciplina == null)
                DbConnection.Instityte.Disciplina.Add(contextDisciplina);
            DbConnection.Instityte.SaveChanges();
            
        }
    }
}
