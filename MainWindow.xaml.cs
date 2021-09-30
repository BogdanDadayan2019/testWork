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

        OracleConnection con = null;

        public MainWindow()
        {         
            SetConnection();       
            InitializeComponent();
        }        
    
        private void UpdateDataGrid()
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
            updCheck = UpdateDataGrid;
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
            AUD(sql, 2);
            CheckUpdateDataGrid();
        }

        private void AUD(String sql_stmt, int state)
        {
            String msg = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql_stmt;
            cmd.CommandType = CommandType.Text;
            msg = "Row Deleted Successfully";

            switch (state)
            {
                case 0:
                    msg = "Row Inserted Successfully!";
                    //cmd.Parameters.Add("GOODS_NAME", OracleDbType.Varchar2, 25).Value = name_goods_txt.Text;
                    //cmd.Parameters.Add("CATEGORY_NAME", OracleDbType.Varchar2, 25).Value = type_goods_txt.Text;
                    //cmd.Parameters.Add("GOODS_PRICE", OracleDbType.Int32, 6).Value = price_goods_txt.Text;
                    //cmd.Parameters.Add("GOODS_DESCRIPTION", OracleDbType.Varchar2, 25).Value = descrip_goods_txt.Text;
                    break;
                case 1:
                    msg = "Row Updated Successfully!";
                    //cmd.Parameters.Add("GOODS_NAME", OracleDbType.Varchar2, 25).Value = name_goods_txt.Text;
                    //cmd.Parameters.Add("CATEGORY_NAME", OracleDbType.Varchar2, 25).Value = type_goods_txt.Text;
                    //cmd.Parameters.Add("GOODS_PRICE", OracleDbType.Int32, 6).Value = price_goods_txt.Text;
                    //cmd.Parameters.Add("GOODS_DESCRIPTION", OracleDbType.Varchar2, 25).Value = descrip_goods_txt.Text;
                    //cmd.Parameters.Add("GOODS_ID", OracleDbType.Int32, 25).Value = id_goods_txt.Text;
                    break;
                case 2:
                    msg = "Row Deleted Successfully";
                    cmd.Parameters.Add("GOODS_ID", OracleDbType.Int32, 6).Value = id_goods_txt.Text;
                    break;
                case 3:
                    msg = "Check Deleted Successfully";
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
                    this.UpdateDataGrid();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GoodsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updGoods += UpdateDataGrid;

            DataGrid dg = sender as DataGrid;
            dr = dg.SelectedItem as DataRowView;

            if (dr != null)
            {

                id_goods_txt.Text = dr["GOODS_ID"].ToString();

            }
        }

        private void GoodsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ChangeGoods changeGoods = new ChangeGoods();
            //changeGoods.Show();
        }

        private void ChekGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
            AUD(sql, 3);
            CheckUpdateDataGrid();
        }

        private void Filtr_Check_BtnClick(object sender, RoutedEventArgs e)
        {
            FilterCheck filter = new FilterCheck();
            filter.Show();
        }

        private void Filtr_Goods_BtnClick(object sender, RoutedEventArgs e)
        {
            FilterGoods filter = new FilterGoods();
            filter.Show();
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
            CheckUpdateDataGrid();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            con.Close();
        }


    }
}

//String sql = "select * from goods where goods_id in (select goods_id from goods where goods_name = :goods_name)"; //имя 

//String sql = "select * from goods where goods_id in (select goods_id from goods where category_name = :category_name)"; //категория

//String sql = "select * from goods where goods_id in (select goods_id from goods where goods_price > :goods_price) "; // прайс от
//String sql = "select * from goods where goods_id in (select goods_id from goods where goods_price < :goods_price) "; // прайс до

//String sql = "select * from goods where goods_id in (select goods_id from goods where goods_price > :goods_price and goods_price < :goods_price) "; //прайс от и до

//String sql = "select * from goods where goods_id in (select goods_id from goods where goods_name = :goods_name and goods_price > :goods_price) ";  // имя, прайс от
//String sql = "select * from goods where goods_id in (select goods_id from goods where goods_name = :goods_name and goods_price < :goods_price) ";  // имя, прайс до

//String sql = "select * from goods where goods_id in (select goods_id from goods where category_name = :category_name and goods_price > :goods_price) ";  // категория, прайс от
//String sql = "select * from goods where goods_id in (select goods_id from goods where category_name = :category_name and goods_price < :goods_price) ";  // категория, прайс до

//String sql = "select * from goods where goods_id in (select goods_id from goods where goods_name = :goods_name and category_name = :category_name)"; // имя и категория

//String sql = "select * from goods where goods_id in (select goods_id from goods where goods_name = :goods_name and goods_price > :goods_price)"; // имя, прайс от
//String sql = "select * from goods where goods_id in (select goods_id from goods where goods_name = :goods_name and goods_price < :goods_price)"; // имя, прайс до

//String sql = "select * from goods where goods_id in (select goods_id from goods where goods_name = :goods_name and goods_price > :goods_price and goods_price < :goods_price) "; // имя, прайс от и до
//String sql = "select * from goods where goods_id in (select goods_id from goods where goods_name = :goods_name and goods_price > :goods_price and category_name = :category_name) "; // имя, категория прайс от 
//String sql = "select * from goods where goods_id in (select goods_id from goods where goods_name = :goods_name and goods_price > :goods_price and category_name = :category_name) "; // имя, категория прайс до


//String sql = "select * from goods where goods_id in (select goods_id from goods where goods_name = :goods_name and goods_price > :goods_price and goods_price < :goods_price and category_name = :category_name) "; // имя, категория прайс от и до

// AUD(sql);