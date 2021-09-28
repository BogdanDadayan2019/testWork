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

    public partial class AddGoodsForCheck : Window
    {
        OracleConnection con = null;
        DataRowView dr;

        public AddGoodsForCheck(DataRowView dr)
        {
            this.dr = dr;
            SetConnection();
            InitializeComponent();
            UpdateLabel();
            UpdateComboBox();
        }     

        private void Change_BtnClick(object sender, RoutedEventArgs e)
        {
            String sql = "INSERT INTO GOODSCHECK2 (CHECK_ID, CATEGORY_NAME, GOODS_NAME, GOODS_PRICE) VALUES(:CHECK_ID, :CATEGORY_NAME, :GOODS_NAME, :GOODS_PRICE)";
            this.AUD(sql);
        }

        private void Close_BtnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateLabel()
        {
            try
            {

                if (dr != null)
                {
                    id_check_txt.Content = dr["CHECK_ID"].ToString();

                }

            }
            catch (Exception)
            {
                String msg = "Выберите чек";
                MessageBox.Show(msg);
            }
        }

        private void UpdateComboBox()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT CATEGORY_NAME FROM CATEGORY";
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            OracleDataAdapter oracleData = new OracleDataAdapter(cmd);
            oracleData.Fill(dt);

            typetxt.ItemsSource = dt.AsDataView();
            typetxt.DisplayMemberPath = dt.Columns[0].ToString();

            cmd.ExecuteNonQuery();

        }

        private void AUD(String sql_stmt)
        {
            String msg = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql_stmt;
            cmd.CommandType = CommandType.Text;

            msg = "Row Inserted Successfully!";
            cmd.Parameters.Add("CHECK_ID", OracleDbType.Varchar2, 25).Value = id_check_txt.Content;
            cmd.Parameters.Add("CATEGORY_NAME", OracleDbType.Varchar2, 25).Value = typetxt.Text;
            cmd.Parameters.Add("GOODS_NAME", OracleDbType.Varchar2, 25).Value = nametxt.Text;
            cmd.Parameters.Add("GOODS_PRICE", OracleDbType.Int32, 6).Value = pricetxt.Text;

            try
            {
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show(msg);
                }
            }
            catch (Exception)
            {

                throw;
            }
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

        private void Window_Closed(object sender, EventArgs e)
        {
            con.Close();
        }

       
    }
}
