using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education_Practike.DBConnection
{
    public partial class Employee
    {
        public bool IsChief
        {
            get
            {
                if (chief == Tab_Number)
                {
                    return true;
                }
                else { return false; }
            }
        }
    }
}
