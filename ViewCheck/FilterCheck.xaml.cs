using Oracle.ManagedDataAccess.Client;
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //String sql = "select * from goods where goods_id in (select goods_id from goods where goods_name = :goods_name and goods_price > :goods_price and goods_price < :goods_price and category_name = :category_name) "; // имя, категория прайс от и до

            AUD();
        }

        private void AUD()
        {
            String msg = "";
            OracleCommand cmd = con.CreateCommand();



            //if (String.IsNullOrWhiteSpace(pricetxt1.Text) & String.IsNullOrWhiteSpace(pricetxtx2.Text) & String.IsNullOrWhiteSpace(categorynametxt.Text))
            //{
            //    ////cmd.Parameters.Add("GOODS_NAME", OracleDbType.Varchar2, 25).Value = nametxt.Text;
            //    ////String sql = "select * from goods where goods_id in (select goods_id from goods where goods_name = :goods_name) "; // имя
            //    ////cmd.CommandText = sql;

            //}
            //else if (true)
            //{

            //}

            cmd.CommandType = CommandType.Text;
            //msg = "Row Updated Successfully!";
            //cmd.Parameters.Add("GOODS_NAME", OracleDbType.Varchar2, 25).Value = nametxt.Text;
            //cmd.Parameters.Add("GOODS_PRICE", OracleDbType.Int32, 25).Value = pricetxt1.Text;
            //cmd.Parameters.Add("GOODS_PRICE", OracleDbType.Int32, 25).Value = pricetxtx2.Text;
            //cmd.Parameters.Add("CATEGORY_NAME", OracleDbType.Varchar2, 25).Value = categorynametxt.Text;

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
            UpdateDataGrid();
        }
    }
}
