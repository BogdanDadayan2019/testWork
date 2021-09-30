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
        OracleConnection con ;

        public FilterGoods()
        {
            con = ConnectionDB.SetConnection();
            InitializeComponent();   
        }      
   
        private void Filter_BtnClick(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            try
            {
                OracleCommand cmd = new OracleCommand("F_FILTER", con);

                cmd.CommandType = CommandType.StoredProcedure;

                List<Goods> goods = new List<Goods>();

                OracleParameter output = cmd.Parameters.Add("l_cursor", OracleDbType.RefCursor);
                cmd.Parameters.Add("g_name", Convert.ToString(nametxt.Text));
                cmd.Parameters.Add("g_type", Convert.ToString(categorynametxt.Text));

                if (pricetxt1.Text == "")
                {
                    cmd.Parameters.Add("g_price1", "0");
                }
                else { cmd.Parameters.Add("g_price1", Convert.ToString(pricetxt1.Text)); }

                if (pricetxt2.Text == "")
                {
                    cmd.Parameters.Add("g_price2", "2147483647");
                }
                else { cmd.Parameters.Add("g_price2", Convert.ToString(pricetxt2.Text)); }


                output.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();

                OracleDataReader reader = ((OracleRefCursor)output.Value).GetDataReader();

                while (reader.Read())
                {
                    Goods lgoods = new Goods();
                    lgoods.GOODS_ID = reader.GetInt32(0);
                    lgoods.CATEGORY_NAME = reader.GetString(2);
                    lgoods.GOODS_NAME = reader.GetString(3);
                    lgoods.GOODS_PRICE = reader.GetInt32(4);
                    lgoods.GOODS_DESCRIPTION = reader.GetString(5);

                    goods.Add(lgoods);

                }
                dataGrid.ItemsSource = goods.ToList();
            }
            catch (OracleException)
            {
                String msg = "Некорекктный ввод";
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
