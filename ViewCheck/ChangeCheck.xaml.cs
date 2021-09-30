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

namespace GoodsCheck.ViewCheck
{
   
    public partial class ChangeCheck : Window
    {

        OracleConnection con;
        MainWindow.UpdDbCheck updDbCheck;
        DataRowView dr;

        public ChangeCheck(MainWindow.UpdDbCheck updCheck, DataRowView dr)
        {
            this.updDbCheck = updCheck;
            this.dr = dr;

            con = ConnectionDB.SetConnection();
            InitializeComponent();        
        }

        private void Сhange_BtnClick(object sender, RoutedEventArgs e)
        {
            String sql = "UPDATE GOODSCHECK2 SET CHECK_DATE = :CHECK_DATE, CHECK_STATUS = :CHECK_STATUS WHERE CHECK_ID = :CHECK_ID";
            AUD(sql);
            updDbCheck();
        }

        private void Cancel_BtnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateTextBox()
        {
            try
            {
                if (dr != null)
                {
                    nametxt.Text = dr["CHECK_ID"].ToString();
                    dataPicker.Text = dr["CHECK_DATE"].ToString();
                    statustxt.Text = dr["CHECK_STATUS"].ToString();
                }
            }
            catch (Exception)
            {
                String msg = "Выберите чек";
                MessageBox.Show(msg);
            }
        }

        private void AUD(String sql_stmt)
        {
            String msg = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql_stmt;
            cmd.CommandType = CommandType.Text;

            msg = "Успешно!";
            
            cmd.Parameters.Add("CHECK_DATE", OracleDbType.Date, 25).Value = dataPicker.SelectedDate;
            cmd.Parameters.Add("CHECK_STATUS", OracleDbType.Varchar2, 25).Value = statustxt.Text;
            cmd.Parameters.Add("CHECK_ID", OracleDbType.Int32, 6).Value = nametxt.Text;

            try
            {
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show(msg);
                    updDbCheck();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTextBox();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            con.Close();
        }
      
    }
}
