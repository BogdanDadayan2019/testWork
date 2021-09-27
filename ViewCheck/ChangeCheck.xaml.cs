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
    /// <summary>
    /// Логика взаимодействия для ChangeCheck.xaml
    /// </summary>
    public partial class ChangeCheck : Window
    {

        OracleConnection con = null;
        MainWindow.UpdDbCheck updDbCheck;

        public ChangeCheck(MainWindow.UpdDbCheck upd1, DataRowView dr)
        {
            this.updDbCheck = upd1;
            SetConnection();
            InitializeComponent();

            try
            {

                if (dr != null)
                {
                    nametxt.Text = dr["CHECK_ID"].ToString();
                    timetxt.Text = dr["CHECK_DATE"].ToString();
                    statustxt.Text = dr["CHECK_STATUS"].ToString();
                    
                }
            }
            catch (Exception)
            {
                String msg = "Выберите чек";
                MessageBox.Show(msg);
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

        private void change_Click(object sender, RoutedEventArgs e)
        {
            String sql = "UPDATE GOODSCHECK2 SET CHECK_ID = :CHECK_ID, CHECK_DATE = :CHECK_DATE, CHECK_STATUS = :GOODS_STATUS";
            this.AUD(sql);

            updDbCheck();

        }

        private void AUD(String sql_stmt)
        {
            String msg = "";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql_stmt;
            cmd.CommandType = CommandType.Text;

            msg = "Row Updated Successfully!";
            cmd.Parameters.Add("CHECK_ID", OracleDbType.Varchar2, 25).Value = nametxt.Text;
            cmd.Parameters.Add("CHECK_DATE", OracleDbType.Date, 25).Value = timetxt.SelectedDate;
            cmd.Parameters.Add("CHECK_STATUS", OracleDbType.Varchar2, 25).Value = statustxt.Text;

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
