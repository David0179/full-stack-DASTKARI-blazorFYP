using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAL
{
    public class DBHelper
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection("Data Source=LAPTOP-1QGUUHKV\\SQLEXPRESS;Initial Catalog=dastkaridb.bacpac;Integrated Security=True;Trust Server Certificate=True");
        }

    }
}
