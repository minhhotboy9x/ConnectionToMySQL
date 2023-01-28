using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ConnectionToMySQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.MouseDown += MouseDownFunction;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(ConfigurationManager.AppSettings["id1"]); 
            //Console.WriteLine(ConfigurationManager.AppSettings["id2"]); 
            try
            {
                string connstring = ConfigurationManager.ConnectionStrings["Test"].ToString();
                //string connstring = "server=127.0.0.1;uid=root;pwd=minh19032002;database=sakila";
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = connstring;
                con.Open();
                string sql = "select * from actor";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
                Console.WriteLine(dataGridView1.Columns.Count);
                con.Close();
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Int32 selectedRowCount =
        dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            
            if (selectedRowCount > 0)
            {
                DataGridViewRow selected_row = dataGridView1.SelectedRows[0];
                textBox1.Text = selected_row.Cells[1].Value.ToString();
            }

            Console.WriteLine($"selectedRowCount {selectedRowCount}");
        }
        private void MouseDownFunction(object sender, MouseEventArgs e)
        {
            dataGridView1.ClearSelection();
            textBox1.Clear();
        }
    }
}
