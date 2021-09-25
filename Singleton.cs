using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsCheck
{
    public class Singleton
    {
        private static Singleton instance;
        public OracleConnection Con;  

        private Singleton()
        {
            Con = new OracleConnection("Data Source=XE;User Id=SYSTEM;Password=name23;");
           
        }

        public static Singleton getInstance()
        {
            if (instance == null)
                instance = new Singleton();
            return instance;
        }
    }
}
