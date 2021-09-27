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
    /// Логика взаимодействия для AddGoods.xaml
    /// </summary>
    public partial class AddGoods : Window
    {
        OracleConnection con = null;
        MainWindow.UpdDbCheck upd1;

        public AddGoods(MainWindow.UpdDbCheck upd1)
        {
            this.upd1 = upd1;
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

        private void change_Click(object sender, RoutedEventArgs e)
        {
            String sql = "INSERT INTO GOODS (GOODS_NAME, CATEGORY_NAME, GOODS_PRICE, GOODS_DESCRIPTION) VALUES(:GOODS_NAME, :CATEGORY_NAME, :GOODS_PRICE, :GOODS_DESCRIPTION)";
            this.AUD(sql);
            upd1();
        }

        private void AUD(String sql_stmt)
        {
            String msg = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql_stmt;
            cmd.CommandType = CommandType.Text;

            msg = "Row Inserted Successfully!";
            cmd.Parameters.Add("GOODS_NAME", OracleDbType.Varchar2, 25).Value = nametxt.Text;
            cmd.Parameters.Add("CATEGORY_NAME", OracleDbType.Varchar2, 25).Value = typetxt.Text;
            cmd.Parameters.Add("GOODS_PRICE", OracleDbType.Varchar2, 25).Value = pricetxt.Text;
            cmd.Parameters.Add("GOODS_DESCRIPTION", OracleDbType.Int32, 6).Value = desctxt.Text;


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
