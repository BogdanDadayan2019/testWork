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
    /// <summary>
    /// Логика взаимодействия для AddGoodsForCheck.xaml
    /// </summary>
    public partial class AddGoodsForCheck : Window
    {
        OracleConnection con = null;

        public AddGoodsForCheck(DataRowView dr)
        {
            SetConnection();
            InitializeComponent();

            if (dr != null)
            {
                id_check_txt.Text = dr["CHECK_ID"].ToString();

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

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            //  data.a = 20;
            
            
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            //  String sql = "insert into goodscheck2 (GOODS_NAME, CATEGORY_NAME, GOODS_PRICE) select GOODS_NAME, CATEGORY_NAME, GOODS_PRICE from goods";
            String sql = "INSERT INTO GOODSCHECK2 (CHECK_ID, CATEGORY_NAME, GOODS_NAME, GOODS_PRICE) VALUES(:CHECK_ID, :CATEGORY_NAME, :GOODS_NAME, :GOODS_PRICE)";
            this.AUD(sql);
        }

        private void AUD(String sql_stmt)
        {
            String msg = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql_stmt;
            cmd.CommandType = CommandType.Text;

            msg = "Row Inserted Successfully!";
            cmd.Parameters.Add("CHECK_ID", OracleDbType.Varchar2, 25).Value = id_check_txt.Text;
            cmd.Parameters.Add("CATEGORY_NAME", OracleDbType.Varchar2, 25).Value = typetxt.Text;
            cmd.Parameters.Add("GOODS_NAME", OracleDbType.Varchar2, 25).Value = nametxt.Text;
            cmd.Parameters.Add("GOODS_PRICE", OracleDbType.Int32, 6).Value = pricetxt.Text;


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

        
    }
}
