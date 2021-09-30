using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsCheck
{
    public class ConnectionDB
    {

        public static OracleConnection SetConnection()
        {
            
            OracleConnection con = new OracleConnection("Data Source=XE;User Id=SYSTEM;Password=name23;");
            try
            {

                con.Open();
                return con;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
