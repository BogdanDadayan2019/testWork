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

namespace GoodsCheck
{

    public partial class ChangeGoods : Window
    {

        OracleConnection con = null;
        MainWindow.UpdDbGoods updDbGoods;

        public ChangeGoods(DataRowView dr, MainWindow.UpdDbGoods updDbGoods)
        {
            this.updDbGoods = updDbGoods;
            
            SetConnection();
            

            InitializeComponent();
            if (dr != null)
            {

                nametxt.Text = dr["GOODS_NAME"].ToString();
                typetxt.Text = dr["CATEGORY_NAME"].ToString();
                pricetxt.Text = dr["GOODS_PRICE"].ToString();
                descriptiontxt.Text = dr["GOODS_DESCRIPTION"].ToString();
                idtxt.Text = dr["GOODS_ID"].ToString();

            }
            UpdateComboBox();
        }

        private void UpdateComboBox()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT CATEGORY_NAME FROM GOODS";
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            OracleDataAdapter oracleData = new OracleDataAdapter(cmd);
            oracleData.Fill(dt);

            typetxt.ItemsSource = dt.AsDataView();
            typetxt.DisplayMemberPath = dt.Columns[0].ToString();

            cmd.ExecuteNonQuery();

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

        private void change_Click(object sender, RoutedEventArgs e)
        {
            String sql = "UPDATE GOODS SET GOODS_NAME = :GOODS_NAME, CATEGORY_NAME = :CATEGORY_NAME, GOODS_PRICE = :GOODS_PRICE, GOODS_DESCRIPTION = :GOODS_DESCRIPTION WHERE GOODS_ID = :GOODS_ID";
            this.AUD(sql);

            updDbGoods();
           
        }

        private void AUD(String sql_stmt)
        {
            String msg = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql_stmt;
            cmd.CommandType = CommandType.Text;

            msg = "Row Updated Successfully!";
            cmd.Parameters.Add("GOODS_NAME", OracleDbType.Varchar2, 25).Value = nametxt.Text;
            cmd.Parameters.Add("CATEGORY_NAME", OracleDbType.Varchar2, 25).Value = typetxt.SelectedItem;
            cmd.Parameters.Add("GOODS_PRICE", OracleDbType.Int32, 6).Value = pricetxt.Text;
            cmd.Parameters.Add("GOODS_DESCRIPTION", OracleDbType.Varchar2, 25).Value = descriptiontxt.Text;
            cmd.Parameters.Add("GOODS_ID", OracleDbType.Int32, 6).Value = idtxt.Text;

            try
            {
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                   // MessageBox.Show(msg);
                    //  this.UpdateDataGrid();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}