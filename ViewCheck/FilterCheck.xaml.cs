using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace GoodsCheck.ViewCheck
{

    public partial class FilterCheck : Window
    {

        OracleConnection con;
        DataRowView dr;
            
        public FilterCheck(DataRowView dr)
        {
            this.dr = dr;
            
            con = ConnectionDB.SetConnection();
            InitializeComponent();
        }

        private void Filtr_BtnClick(object sender, RoutedEventArgs e)
        {
             UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            try
            {
                OracleCommand cmd = new OracleCommand("F_FILTER_CHECK", con);

                cmd.CommandType = CommandType.StoredProcedure;

                List<Check> check = new List<Check>();

                OracleParameter output = cmd.Parameters.Add("l_cursor", OracleDbType.RefCursor);

                cmd.Parameters.Add("c_id", Convert.ToString(name_txt.Text));
                if (datePicker.Text == "")
                {
                    cmd.Parameters.Add("c_date", null);
                }
                else { cmd.Parameters.Add("c_date", Convert.ToDateTime(datePicker.Text)); }

                if (price_txt1.Text == "")
                {
                    cmd.Parameters.Add("c_price_min", "0");
                }
                else { cmd.Parameters.Add("c_price_min", Convert.ToString(price_txt1.Text)); }

                if (price_txt2.Text == "")
                {
                    cmd.Parameters.Add("c_price_max", "2147483647");
                }
                else { cmd.Parameters.Add("c_price_max", Convert.ToString(price_txt2.Text)); }

                output.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();

                OracleDataReader reader = ((OracleRefCursor)output.Value).GetDataReader();

                while (reader.Read())
                {

                    Check lcheck = new Check();

                    lcheck.CHECK_ID = reader.GetString(0);
                    lcheck.CHECK_DATE = reader.GetDateTime(1);
                    lcheck.CHECK_STATUS = reader.GetString(2);

                    check.Add(lcheck);

                }

                filterDataGrid.ItemsSource = check.ToList();

            }
            catch (OracleException)
            {

                String msg = "Некорректный ввод";
                MessageBox.Show(msg);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            UpdateDataGrid();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            con.Close();
        }
    }
}
