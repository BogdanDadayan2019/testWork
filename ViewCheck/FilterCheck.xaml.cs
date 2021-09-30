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
        OracleConnection con = null;

        public FilterCheck()
        {
            SetConnection();
            InitializeComponent();
        }

        private void UpdateDataGrid()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT CHECK_ID, CHECK_DATE, CHECK_STATUS FROM GOODSCHECK2";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGrid.ItemsSource = dt.DefaultView;
            dr.Close();
        }

        private void SetConnection()
        {
            con = new OracleConnection("Data Source=XE;User Id=SYSTEM;Password=name23;");
            try
            {
                con.Open();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Filtr_BtnClick(object sender, RoutedEventArgs e)
        {
                UpdateDataGrid2();
        }

        private void UpdateDataGrid2()
        {
            try
            {
                OracleCommand cmd = new OracleCommand("F_FILTER_CHECK", con);

                cmd.CommandType = CommandType.StoredProcedure;

                List<Check> check = new List<Check>();

                OracleParameter output = cmd.Parameters.Add("l_cursor", OracleDbType.RefCursor);

                cmd.Parameters.Add("c_number", Convert.ToString(nametxt.Text));            
                cmd.Parameters.Add("c_date", Convert.ToDateTime(datePicker.Text));
                


                output.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();

                OracleDataReader reader = ((OracleRefCursor)output.Value).GetDataReader();
                int i = 5;

                while (reader.Read())
                {
                    i++;

                    Check lcheck = new Check();
                    lcheck.Check_id = reader.GetString(0);

                    lcheck.Check_date = reader.GetDateTime(1);


                    check.Add(lcheck);

                }

                dataGrid.ItemsSource = check.ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AUD()
        {
            String msg = "";
            OracleCommand cmd = con.CreateCommand();

            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGrid.ItemsSource = dt.DefaultView;
            dr.Close();

            try
            {
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show(msg);
                    //  this.UpdateDataGrid();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //UpdateDataGrid();
        }
    }
}
