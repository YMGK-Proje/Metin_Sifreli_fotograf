using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    class sqlBaglantisi
    {
        public SqlConnection Baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=LAPTOP-50BUGHO6\\SQLEXPRESS;Initial Catalog=OnlineChat;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
