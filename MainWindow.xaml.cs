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

        OracleConnection con = null;

        public MainWindow()
        {
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

        private void UpdateComboBox() 
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT CATEGORY_NAME FROM GOODS";
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            OracleDataAdapter oracleData = new OracleDataAdapter(cmd);
            oracleData.Fill(dt);

            type_goods_txt.ItemsSource = dt.AsDataView();
            type_goods_txt.DisplayMemberPath = dt.Columns[0].ToString();

            cmd.ExecuteNonQuery();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
            UpdateComboBox();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            con.Close();
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            String sql = "INSERT INTO GOODS (GOODS_NAME, CATEGORY_NAME, GOODS_PRICE, GOODS_DESCRIPTION) VALUES(:GOODS_NAME, :CATEGORY_NAME, :GOODS_PRICE, :GOODS_DESCRIPTION)";
            this.AUD(sql, 0);
            add_btn.IsEnabled = false;
            upd_btn.IsEnabled = true;
            delete_btn.IsEnabled = true;
        }

        private void upd_btn_Click(object sender, RoutedEventArgs e)
        {
            String sql = "UPDATE GOODS SET GOODS_NAME = :GOODS_NAME, CATEGORY_NAME = :CATEGORY_NAME, GOODS_PRICE = :GOODS_PRICE, GOODS_DESCRIPTION = :GOODS_DESCRIPTION WHERE GOODS_ID = :GOODS_ID";
            this.AUD(sql, 1);
        }
      
        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            String sql = "DELETE FROM GOODS WHERE GOODS_ID = :GOODS_ID";
            this.AUD(sql, 2);

        }

        private void AUD(String sql_stmt, int state)
        {
            String msg = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql_stmt;
            cmd.CommandType = CommandType.Text;

            switch (state)
            {
                case 0:
                    msg = "Row Inserted Successfully!";
                    cmd.Parameters.Add("GOODS_NAME", OracleDbType.Varchar2, 25).Value = name_goods_txt.Text;
                    cmd.Parameters.Add("CATEGORY_NAME", OracleDbType.Varchar2, 25).Value = type_goods_txt.Text;
                    cmd.Parameters.Add("GOODS_PRICE", OracleDbType.Int32, 6).Value = price_goods_txt.Text;
                    cmd.Parameters.Add("GOODS_DESCRIPTION", OracleDbType.Varchar2, 25).Value = descrip_goods_txt.Text;
                    break;
                case 1:
                    msg = "Row Updated Successfully!";
                    cmd.Parameters.Add("GOODS_NAME", OracleDbType.Varchar2, 25).Value = name_goods_txt.Text;
                    cmd.Parameters.Add("CATEGORY_NAME", OracleDbType.Varchar2, 25).Value = type_goods_txt.Text;
                    cmd.Parameters.Add("GOODS_PRICE", OracleDbType.Int32, 6).Value = price_goods_txt.Text;
                    cmd.Parameters.Add("GOODS_DESCRIPTION", OracleDbType.Varchar2, 25).Value = descrip_goods_txt.Text;
                    cmd.Parameters.Add("GOODS_ID", OracleDbType.Int32, 25).Value = id_goods_txt.Text;
                    break;
                case 2:
                    msg = "Row Deleted Successfully";
                    cmd.Parameters.Add("GOODS_ID", OracleDbType.Int32, 25).Value = id_goods_txt.Text; 
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

        private void goodsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                name_goods_txt.Text = dr["GOODS_NAME"].ToString();
                type_goods_txt.Text = dr["CATEGORY_NAME"].ToString();
                price_goods_txt.Text = dr["GOODS_PRICE"].ToString();
                descrip_goods_txt.Text = dr["GOODS_DESCRIPTION"].ToString();
                id_goods_txt.Text = dr["GOODS_ID"].ToString();

                add_btn.IsEnabled = false;
                upd_btn.IsEnabled = true;
                delete_btn.IsEnabled = true;

            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ChangeGoodsWindow changeGoodsWindow = new ChangeGoodsWindow();

            changeGoodsWindow.Show();
        }
    }
}
