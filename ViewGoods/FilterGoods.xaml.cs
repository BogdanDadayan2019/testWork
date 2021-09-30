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

namespace GoodsCheck.ViewGoods
{
    
    public partial class FilterGoods : Window
    {
        OracleConnection con = null;

        public FilterGoods()
        {
            SetConnection();
            InitializeComponent();

            pricetxt1.Text = "1";
            pricetxtx2.Text = "9999";
        }

        private void UpdateDataGrid()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT CATEGORY_NAME, GOODS_NAME, GOODS_PRICE FROM GOODS";
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //UpdateDataGrid2();
           // UpdateDataGrid();
        }

      

        private void Filter_BtnClick(object sender, RoutedEventArgs e)
        {
            //String sql = "select * from goods where goods_id in (select goods_id from goods where goods_name = :goods_name and goods_price > :goods_price and goods_price < :goods_price and category_name = :category_name) "; // имя, категория прайс от и до
            UpdateDataGrid2();
            //AUD();
        }

        private void AUD()
        {
            String msg = ""; 
            OracleCommand cmd = con.CreateCommand();
            
            

            if (String.IsNullOrWhiteSpace(pricetxt1.Text) & String.IsNullOrWhiteSpace(pricetxtx2.Text) & String.IsNullOrWhiteSpace(categorynametxt.Text))
            {
                ////cmd.Parameters.Add("GOODS_NAME", OracleDbType.Varchar2, 25).Value = nametxt.Text;
                ////String sql = "select * from goods where goods_id in (select goods_id from goods where goods_name = :goods_name) "; // имя
                ////cmd.CommandText = sql;

            } 
            else if(true)
            {

            }

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

        private void UpdateDataGrid2()
        {
            try
            {
                OracleCommand cmd = new OracleCommand("F_FILTER", con);


                //cmd.Parameters.Add("20", OracleDbType.Int32, 25).ParameterName = "PARAM2";

                //cmd.Parameters.Add("PARAM2", OracleDbType.Int32, 25).Value = pricetxt1.Text;

                cmd.CommandType = CommandType.StoredProcedure;

                List<Goods> goods = new List<Goods>();

                //OracleParameter input = cmd.Parameters.Add("")

                //cmd.Parameters.Add("GOODS_NAME", OracleDbType.Varchar2, 25).Value = nametxt.Text;
                ////String sql = "select * from goods where goods_id in (select goods_id from goods where goods_name = :goods_name) "; // имя
                ////cmd.CommandText = sql;




                //string x = pricetxtx2.Text;
                //if (String.IsNullOrWhiteSpace(pricetxtx2.Text))
                //{
                //    x = "-1";
                //}
                OracleParameter output = cmd.Parameters.Add("l_cursor", OracleDbType.RefCursor);
                cmd.Parameters.Add("g_name", Convert.ToString(nametxt.Text));
                cmd.Parameters.Add("g_type", Convert.ToString(categorynametxt.Text));
                cmd.Parameters.Add("g_price1", Convert.ToString(pricetxt1.Text));
                cmd.Parameters.Add("g_price2", Convert.ToString(pricetxtx2.Text));

                //cmd.Parameters.Add("NAME1", Convert.ToString(nametxt.Text));
                //cmd.Parameters.Add("PRICE2", Convert.ToInt32(x));

                output.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();

                OracleDataReader reader = ((OracleRefCursor)output.Value).GetDataReader();
               // goods.Clear();

                while (reader.Read())
                {
                    Goods lgoods = new Goods();
                    lgoods.Goods_id = reader.GetInt32(0);
                    lgoods.Category_name = reader.GetString(2);
                    lgoods.Goods_name = reader.GetString(3);
                    lgoods.Goods_price = reader.GetInt32(4);
                    lgoods.Goods_description = reader.GetString(5);

                    goods.Add(lgoods);

                }

                dataGrid.ItemsSource = goods.ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}
