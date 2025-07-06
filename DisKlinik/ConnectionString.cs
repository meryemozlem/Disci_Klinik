using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DisKlinik
{
    class ConnectionString
    {
        public SqlConnection GetCon() 
        {
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString= //kendi yerel db kısmınızı yapıştırın.;
            return baglanti;

        }
    }
}
