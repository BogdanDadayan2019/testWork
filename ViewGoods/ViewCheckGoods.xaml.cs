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

namespace GoodsCheck.ViewGoods
{
    /// <summary>
    /// Логика взаимодействия для ViewCheckGoods.xaml
    /// </summary>
    public partial class ViewCheckGoods : Window
    {
        OracleConnection con = null;
        DataRowView dr;
        string a;

        public ViewCheckGoods(DataRowView dr)
        {
            this.dr = dr;

            SetConnection();
            InitializeComponent();
            UpdateLabel();

        }

        private void UpdateLabel()
        {
            try
            {
                if (dr != null)
                {
                    a = dr["CHECK_ID"].ToString();
                    label_idcheck.Content = a.ToString();

                }
            }
            catch { }
        }

        private void UpdateDataGrid()
        {
            
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"SELECT GOODS_NAME, CATEGORY_NAME, GOODS_PRICE FROM GOODSCHECK2 WHERE CHECK_ID = {a}";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            CheckGrid.ItemsSource = dt.DefaultView;
            UpdateSum();
            //var sum = 0;
            //foreach (DataRow item in dt.Rows)
            //{
            //    var c = item.ItemArray[2];
            //    sum += Int32.Parse(c.ToString());
            //}
            //sumcheck.Text = sum.ToString();

            dr.Close();
                 
        }

        private void UpdateSum()
        {

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"SELECT check_id , SUM(goods_price) FROM goodscheck2 WHERE check_id={a} GROUP BY check_id";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            dt.Load(dr);

            var x = dt.Rows[0].ItemArray[1];

            sumcheck.Text = x.ToString();

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
            UpdateDataGrid();                    
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            con.Close();
        }
    }
}

//b = dr["GOODS_PRICE"].ToString();
//foreach (var item in dr["GOODS_PRICE"])
//{

//}

//idcheck1.Text = dr["CHECK_DATE"].ToString();
//idcheck2.Text = dr["CHECK_STATUS"].ToString();