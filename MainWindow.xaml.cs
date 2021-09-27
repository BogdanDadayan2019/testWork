﻿using GoodsCheck.ViewCheck;
using GoodsCheck.ViewGoods;
using Oracle.ManagedDataAccess.Client;
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

        UpdDbCheck upd1;
        UpdDbGoods updGoods;
        DataRowView dr;

        OracleConnection con = null;

        public MainWindow()
        {
            
           // upd = CheckUpdateDataGrid();
            SetConnection();       
            InitializeComponent();
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

        private void UpdateDataGrid()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT GOODS_ID, CATEGORY_ID, CATEGORY_NAME, GOODS_NAME, GOODS_PRICE, GOODS_DESCRIPTION FROM GOODS";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            goodsGrid.ItemsSource = dt.DefaultView;
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
            chekGrid.ItemsSource = dt.DefaultView;
            dr.Close();
        }

        //private void UpdateComboBox()
        //{
        //    OracleCommand cmd = con.CreateCommand();
        //    cmd.CommandText = "SELECT CATEGORY_NAME FROM GOODS";
        //    cmd.CommandType = CommandType.Text;
        //    DataTable dt = new DataTable();
        //    OracleDataAdapter oracleData = new OracleDataAdapter(cmd);
        //    oracleData.Fill(dt);

        //    //te.ItemsSource = dt.AsDataView();
        //    //comboBox.DisplayMemberPath = dt.Columns[0].ToString();

        //    cmd.ExecuteNonQuery();

        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            
            UpdateDataGrid();
            CheckUpdateDataGrid();
            //UpdateComboBox();

            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            con.Close();
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            //String sql = "INSERT INTO GOODS (GOODS_NAME, CATEGORY_NAME, GOODS_PRICE, GOODS_DESCRIPTION) VALUES(:GOODS_NAME, :CATEGORY_NAME, :GOODS_PRICE, :GOODS_DESCRIPTION)";
            //this.AUD(sql, 1);
            //add_btn.IsEnabled = false;
            //upd_btn.IsEnabled = true;
            //delete_btn.IsEnabled = true;

            upd1 = UpdateDataGrid;
            AddGoods check = new AddGoods(upd1);
            check.Show();
        }

        private void upd_btn_Click(object sender, RoutedEventArgs e)
        {        
            ChangeGoods changeGoods = new ChangeGoods(dr, updGoods);
            changeGoods.Show();
        }
      
        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            String sql = "DELETE FROM GOODS WHERE GOODS_ID = :GOODS_ID";
            AUD(sql, 2);
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

        public void goodsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updGoods += UpdateDataGrid;
            //updGoods += UpdateComboBox;

            DataGrid dg = sender as DataGrid;
            dr = dg.SelectedItem as DataRowView;

            if (dr != null)
            {

                id_goods_txt.Text = dr["GOODS_ID"].ToString();
                

                add_btn.IsEnabled = false;
                upd_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;

            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("1");
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            
           
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void goodsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ChangeGoods changeGoods = new ChangeGoods();
            //changeGoods.Show();
        }

        private void add_check_btn(object sender, RoutedEventArgs e)
        {
            upd1 = CheckUpdateDataGrid;
            CheckWindow check = new CheckWindow(upd1);
            check.Show();
        }

        private void add_for_check_btn(object sender, RoutedEventArgs e)
        {
            AddGoodsForCheck changeGoods = new AddGoodsForCheck(dr);
            changeGoods.Show();
        }

        private void change_check_btn(object sender, RoutedEventArgs e)
        {
            upd1 = CheckUpdateDataGrid;
            ChangeCheck check = new ChangeCheck(upd1, dr);
            check.Show();
        }

        private void delete_check_btn(object sender, RoutedEventArgs e)
        {
            String sql = "DELETE FROM GOODSCHECK2 WHERE CHECK_ID = :CHECK_ID";
            AUD(sql, 3);
        }

        private void chekGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {

                id_check_txt.Text = dr["CHECK_ID"].ToString();

            }
            

        }

        private void chekGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewCheckGoods changeGoods = new ViewCheckGoods(dr);
            changeGoods.Show();
        }

        private void filtr_btn_Click(object sender, RoutedEventArgs e)
        {

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

            FilterGoods filter = new FilterGoods();
            filter.Show();

        }

        private void FiltrGoods()
        {
           

        }

        private void AUD(String sql_stmt)
        {
            String msg = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql_stmt;
            cmd.CommandType = CommandType.Text;

            //if (filtr_name.Text == "")
            //{

            //} if else { filtr_name.}


            msg = "Row Updated Successfully!";
            //cmd.Parameters.Add("GOODS_NAME", OracleDbType.Varchar2, 25).Value = filtr_name.Text;
            //cmd.Parameters.Add("GOODS_PRICE", OracleDbType.Int32, 25).Value = filtr_price1.Text;
            //cmd.Parameters.Add("GOODS_PRICE", OracleDbType.Int32, 25).Value = filtr_price2.Text;
            //cmd.Parameters.Add("CATEGORY_NAME", OracleDbType.Varchar2, 25).Value = filtr_category.Text;



            //cmd.CommandText = "select * from goods where goods_id in (select goods_id from goods where goods_name = :goods_name)";
            //cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            goodsGrid.ItemsSource = dt.DefaultView;
            dr.Close();
            //cmd.Parameters.Add("CHECK_DATE", OracleDbType.Date, 25).Value = filtr_price.SelectedDate;


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

        private void filter_check_btn(object sender, RoutedEventArgs e)
        {
            FilterCheck filter = new FilterCheck();
            filter.Show();
        }
    }
}

//switch (state)
//{
//    case 0:
//        msg = "Row Inserted Successfully!";
//        cmd.Parameters.Add("GOODS_NAME", OracleDbType.Varchar2, 25).Value = name_goods_txt.Text;
//        cmd.Parameters.Add("CATEGORY_NAME", OracleDbType.Varchar2, 25).Value = type_goods_txt.Text;
//        cmd.Parameters.Add("GOODS_PRICE", OracleDbType.Int32, 6).Value = price_goods_txt.Text;
//        cmd.Parameters.Add("GOODS_DESCRIPTION", OracleDbType.Varchar2, 25).Value = descrip_goods_txt.Text;
//        break;
//    case 1:
//        msg = "Row Updated Successfully!";
//        cmd.Parameters.Add("GOODS_NAME", OracleDbType.Varchar2, 25).Value = name_goods_txt.Text;
//        cmd.Parameters.Add("CATEGORY_NAME", OracleDbType.Varchar2, 25).Value = type_goods_txt.Text;
//        cmd.Parameters.Add("GOODS_PRICE", OracleDbType.Int32, 6).Value = price_goods_txt.Text;
//        cmd.Parameters.Add("GOODS_DESCRIPTION", OracleDbType.Varchar2, 25).Value = descrip_goods_txt.Text;
//        cmd.Parameters.Add("GOODS_ID", OracleDbType.Int32, 25).Value = id_goods_txt.Text;
//        break;
//    case 2:
//        msg = "Row Deleted Successfully";
//        cmd.Parameters.Add("GOODS_ID", OracleDbType.Int32, 25).Value = id_goods_txt.Text; 
//        break;
//}
//try
//{
//    int n = cmd.ExecuteNonQuery();
//    if (n > 0)
//    {
//        MessageBox.Show(msg);
//        this.UpdateDataGrid();
//    }
//}
//catch (Exception)
//{

//    throw;
//}