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
    
    public partial class ViewCheckGoods : Window
    {
        OracleConnection con;
        DataRowView dr;
        string a;

        public ViewCheckGoods(DataRowView dr)
        {
            this.dr = dr;

            con = ConnectionDB.SetConnection();
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
            cmd.CommandText = $"SELECT GOODS_NAME, CATEGORY_NAME, GOODS_PRICE FROM goods_in_check WHERE CHECK_ID = {a}";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            CheckGrid.ItemsSource = dt.DefaultView;
            UpdateSum();
            dr.Close();

        }

        private void UpdateSum()
        {

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = $"SELECT check_id , SUM(goods_price) FROM goods_in_check WHERE check_id={a} GROUP BY check_id";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            dt.Load(dr);
            if (dt.Rows.Count == 0)
            {
              var x = 0;
              sumcheck.Text = x.ToString();
            }
            else
            {
                var x = dt.Rows[0].ItemArray[1];
                sumcheck.Text = x.ToString();
            }            
            dr.Close();

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

