using Education_Practike.DBConnection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education_Practike.Function
{
    internal class AuthorizationFunc
    {
        public static ObservableCollection<Employee> employees { get; set; }
        public static Employee AuthorizationEmpl(string login, string password)
        {
            employees = new ObservableCollection<Employee>(DbConnection.Instityte.Employee.ToList());
            var userExists = employees.Where(user => user.Login == login && user.Password == password).FirstOrDefault();
            if (userExists != null)
            {
                return userExists;
            }
            else
            {
                return userExists;
            }
        }
    }
}
