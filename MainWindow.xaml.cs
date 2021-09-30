using GoodsCheck.ViewCheck;
using GoodsCheck.ViewGoods;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GoodsCheck
{

    public partial class MainWindow : Window
    {
        public delegate void UpdDbCheck();
        public delegate void UpdDbGoods();

        UpdDbCheck updCheck;
        UpdDbGoods updGoods;
        DataRowView dr;

        OracleConnection con;

        public MainWindow()
        {
            con = ConnectionDB.SetConnection();
            InitializeComponent();   
        }        
    
        private void GoodsUpdateDataGrid()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT GOODS_ID, CATEGORY_ID, CATEGORY_NAME, GOODS_NAME, GOODS_PRICE, GOODS_DESCRIPTION FROM GOODS";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            GoodsGrid.ItemsSource = dt.DefaultView;
            dr.Close();
        }

        public void CheckUpdateDataGrid()
        {
            
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT CHECK_ID, CHECK_DATE, CHECK_STATUS FROM GOODSCHECK2";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            ChekGrid.ItemsSource = dt.DefaultView;
            dr.Close();
        }
      
        private void Add_Goods_BtnClick(object sender, RoutedEventArgs e)
        {
            updCheck = GoodsUpdateDataGrid;
            AddGoods check = new AddGoods(updCheck);
            check.Show();
        }

        private void Update_Goods_BtnClick(object sender, RoutedEventArgs e)
        {        
            ChangeGoods changeGoods = new ChangeGoods(dr, updGoods);
            changeGoods.Show();
        }
      
        private void Delete_Goods_BtnClick(object sender, RoutedEventArgs e)
        {
            String sql = "DELETE FROM GOODS WHERE GOODS_ID = :GOODS_ID";
            AUD(sql, 0);
            CheckUpdateDataGrid();
        }

        private void Filtr_Goods_BtnClick(object sender, RoutedEventArgs e)
        {
            FilterGoods filter = new FilterGoods();
            filter.Show();
        }

        public void GoodsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdGoodsBtn.IsEnabled = true;

            DeleteGoodsBtn.IsEnabled = true;

            updGoods += GoodsUpdateDataGrid;

            DataGrid dg = sender as DataGrid;
            dr = dg.SelectedItem as DataRowView;

            if (dr != null)
            {
                id_goods_txt.Text = dr["GOODS_ID"].ToString();
            }
        }

        private void AUD(String sql_stmt, int state)
        {
            String msg = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql_stmt;
            cmd.CommandType = CommandType.Text;
            msg = "Успешно удалено!";

            switch (state)
            {
                case 0:
                    msg = "Успешно удалено!";
                    cmd.Parameters.Add("GOODS_ID", OracleDbType.Int32, 6).Value = id_goods_txt.Text;
                    break;
                case 1:
                    msg = "Успешно удалено!";
                        cmd.Parameters.Add("CHECK_ID", OracleDbType.Int32, 6).Value = id_check_txt.Text;
                    CheckUpdateDataGrid();
                    break;
            }
            try
            {
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show(msg);
                    GoodsUpdateDataGrid();
                }
            }
            catch (FormatException)
            {
                msg = "Выберите поле!";
                MessageBox.Show(msg);
            }
        }
  
        private void ChekGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeCheckBtn.IsEnabled = true;
            DeleteCheckBtn.IsEnabled = true;
            AddForCheckBtn.IsEnabled = true;

            DataGrid dg = sender as DataGrid;
            dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                id_check_txt.Text = dr["CHECK_ID"].ToString();
            }
        }

        private void ChekGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewCheckGoods changeGoods = new ViewCheckGoods(dr);
            changeGoods.Show();
        }

        private void Add_Check_BtnClick(object sender, RoutedEventArgs e)
        {
            updCheck = CheckUpdateDataGrid;
            CheckWindow check = new CheckWindow(updCheck);
            check.Show();
        }

        private void Add_For_Check_BtnClick(object sender, RoutedEventArgs e)
        {
            AddGoodsForCheck changeGoods = new AddGoodsForCheck(dr);
            changeGoods.Show();
        }

        private void Update_Check_BtnClick(object sender, RoutedEventArgs e)
        {
            updCheck = CheckUpdateDataGrid;
            ChangeCheck check = new ChangeCheck(updCheck, dr);
            check.Show();
        }

        private void Delete_Check_BtnClick(object sender, RoutedEventArgs e)
        {

            String sql = "DELETE FROM GOODSCHECK2 WHERE CHECK_ID = :CHECK_ID";
            AUD(sql, 1);
            CheckUpdateDataGrid();
        }

        private void Filtr_Check_BtnClick(object sender, RoutedEventArgs e)
        {
            FilterCheck filter = new FilterCheck(dr);
            filter.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdGoodsBtn.IsEnabled = false;
            DeleteGoodsBtn.IsEnabled = false;

            ChangeCheckBtn.IsEnabled = false;
            DeleteCheckBtn.IsEnabled = false;
            AddForCheckBtn.IsEnabled = false;

            GoodsUpdateDataGrid();
            CheckUpdateDataGrid();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            con.Close();
        }
    }
}

