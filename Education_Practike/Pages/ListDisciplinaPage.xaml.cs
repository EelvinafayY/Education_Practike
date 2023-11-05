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
    /// Логика взаимодействия для ListDisciplinaPage.xaml
    /// </summary>
    public partial class ListDisciplinaPage : Page
    {

        public static List<Disciplina> disciplinas { get; set; }
        public static List<Department> departments { get; set; }
        
        public ListDisciplinaPage()
        {
            InitializeComponent();
            disciplinas = new List<Disciplina>(DBConnection.DbConnection.Instityte.Disciplina.ToList());
            departments = new List<Department>(DBConnection.DbConnection.Instityte.Department.ToList());
            this.DataContext = this;
        }

        private void BackBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

        private void Refresh()
        {
            DisciplinaLV.ItemsSource = disciplinas;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
