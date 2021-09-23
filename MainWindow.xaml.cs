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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            con.Close();
        }
    }
}
