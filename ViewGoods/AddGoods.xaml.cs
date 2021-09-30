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

    public partial class AddGoods : Window
    {
        OracleConnection con;
        MainWindow.UpdDbCheck UpdateDbGoods;

        public AddGoods(MainWindow.UpdDbCheck upd1)
        {
            this.UpdateDbGoods = upd1;

            con = ConnectionDB.SetConnection();
            InitializeComponent();
        }
    
        private void Add_BtnClick(object sender, RoutedEventArgs e)
        {
            String sql = "INSERT INTO GOODS (GOODS_NAME, CATEGORY_NAME, GOODS_PRICE, GOODS_DESCRIPTION) VALUES(:GOODS_NAME, :CATEGORY_NAME, :GOODS_PRICE, :GOODS_DESCRIPTION)";
            this.AUD(sql);
            UpdateDbGoods();
        }

        private void Cancel_BtnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateComboBox()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT CATEGORY_NAME FROM CATEGORY";
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            OracleDataAdapter oracleData = new OracleDataAdapter(cmd);
            oracleData.Fill(dt);

            type_txt.ItemsSource = dt.AsDataView();
            type_txt.DisplayMemberPath = dt.Columns[0].ToString();

            cmd.ExecuteNonQuery();
        }

        private void AUD(String sql_stmt)
        {
            String msg = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql_stmt;
            cmd.CommandType = CommandType.Text;

            msg = "Успешно!";
            cmd.Parameters.Add("GOODS_NAME", OracleDbType.Varchar2, 25).Value = name_txt.Text;
            cmd.Parameters.Add("CATEGORY_NAME", OracleDbType.Varchar2, 25).Value = type_txt.Text;
            cmd.Parameters.Add("GOODS_PRICE", OracleDbType.Varchar2, 6).Value = price_txt.Text;
            cmd.Parameters.Add("GOODS_DESCRIPTION", OracleDbType.Varchar2, 25).Value = desc_txt.Text;

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
                String msg1 = "Некорекктный ввод";
                MessageBox.Show(msg1);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateComboBox();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            con.Close();
        }

    }
}
