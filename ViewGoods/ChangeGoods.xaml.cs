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

        OracleConnection con;
        DataRowView dr;
        MainWindow.UpdDbGoods updDbGoods;

        public ChangeGoods(DataRowView dr, MainWindow.UpdDbGoods updDbGoods)
        {
            this.dr = dr;
            this.updDbGoods = updDbGoods;

            con = ConnectionDB.SetConnection();
            InitializeComponent();
        }

        private void Change_BtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                String sql = "UPDATE GOODS SET GOODS_NAME = :GOODS_NAME, CATEGORY_NAME = :CATEGORY_NAME, GOODS_PRICE = :GOODS_PRICE, GOODS_DESCRIPTION = :GOODS_DESCRIPTION WHERE GOODS_ID = :GOODS_ID";
                this.AUD(sql);
                
            }
            catch (NullReferenceException)
            {
                string msg = "Вы не выбрали товар";
                MessageBox.Show(msg);
            }
        }

        private void Cancel_BtnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateField()
        {
            try
            {
                if (dr != null)
            {
                name_txt.Text = dr["GOODS_NAME"].ToString();
                type_txt.Text = dr["CATEGORY_NAME"].ToString();
                price_txt.Text = dr["GOODS_PRICE"].ToString();
                descr_txt.Text = dr["GOODS_DESCRIPTION"].ToString();
                id_txt.Text = dr["GOODS_ID"].ToString();
            }
            UpdateComboBox();
            }
            catch (ArgumentException)
            {
                string msg = "Выберите элемент";
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
            cmd.Parameters.Add("GOODS_PRICE", OracleDbType.Int32, 6).Value = price_txt.Text;
            cmd.Parameters.Add("GOODS_DESCRIPTION", OracleDbType.Varchar2, 25).Value = descr_txt.Text;
            cmd.Parameters.Add("GOODS_ID", OracleDbType.Int32, 6).Value = id_txt.Text;

            try
            {
               
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    updDbGoods();
                    MessageBox.Show(msg);                 
                }
            }
            catch (NullReferenceException)
            {
                msg = "Некорекктный ввод";
                MessageBox.Show(msg);
            }
            catch (Exception ex)
            {
                msg = "Ошибка!";
                MessageBox.Show(msg);
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {         
            UpdateField();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            con.Close();
        }

    }
}