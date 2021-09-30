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

    public partial class CheckWindow : Window
    {
        OracleConnection con;
        private MainWindow.UpdDbCheck updCheck;
      
        public CheckWindow(MainWindow.UpdDbCheck updCheck)
        {
            this.updCheck = updCheck;

            con = ConnectionDB.SetConnection();
            InitializeComponent();
        }
     
        private void Add_BtnClick(object sender, RoutedEventArgs e)
        {
            String sql = "INSERT INTO GOODSCHECK2 (CHECK_ID, CHECK_DATE, CHECK_STATUS) VALUES(:CHECK_ID, :CHECK_DATE, :CHECK_STATUS)";
            AUD(sql);
            updCheck();
        }

        private void Cancel_BtnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AUD(String sql_stmt)
        {
            String msg = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql_stmt;
            cmd.CommandType = CommandType.Text;

            msg = "Успешно!";
            cmd.Parameters.Add("CHECK_ID", OracleDbType.Varchar2, 25).Value = id_check_txt.Text;
            cmd.Parameters.Add("CHECK_DATE", OracleDbType.Date, 7).Value = time_check_txt.SelectedDate;
            cmd.Parameters.Add("CHECK_STATUS", OracleDbType.Varchar2, 6).Value = status_check_txt.Text;

            try
            {
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show(msg);
                }
            }
            catch (OracleException)
            {
                String msg1 = "Некорекктный ввод";
                MessageBox.Show(msg1);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            con.Close();
        }
       
    }
}