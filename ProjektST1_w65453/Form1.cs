using ProjektST1_w65453.ToDoClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektST1_w65453
{
    public partial class ToDo : Form
    {
        public ToDo()
        {
            InitializeComponent();
        }

        taskClass t = new taskClass();

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            //pobieranie wartości z pól
            t.Task = txtBoxTask.Text;
            t.TaskDescription = txtBoxTaskDescription.Text;
            t.Status = cmbStatus.Text;
            t.Date = dateTimePicker.Text;

            bool success = t.Insert(t);
            if (success = true)
            {
                MessageBox.Show("Dodano nowe zadanie");
                Clear();
            }
            else
            {
                MessageBox.Show("Nie udało sie dodać nowego zadania");
            }
            DataTable dt = t.Select();
            dgvTaskList.DataSource = dt;
        }

        private void ToDo_Load(object sender, EventArgs e)
        {
            DataTable dt = t.Select();
            dgvTaskList.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Clear()
        {
            txtBoxTask.Text = "";
            txtBoxTaskDescription.Text = "";
            cmbStatus.Text = "";
            dateTimePicker.Text = "";
            txtBoxTaskID.Text= "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            t.TaskID = int.Parse(txtBoxTaskID.Text);
            t.Task = txtBoxTask.Text;
            t.TaskDescription = txtBoxTaskDescription.Text;
            t.Status = cmbStatus.Text;
            t.Date = dateTimePicker.Text;

            bool success = t.Update(t);
            if (success = true)
            {
                MessageBox.Show("Uaktualniono zadanie");
                DataTable dt = t.Select();
                dgvTaskList.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Nie udało sie uaktualnić zadania");
            }
        }

        private void dgvTaskList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtBoxTaskID.Text = dgvTaskList.Rows[rowIndex].Cells[0].Value.ToString();
            txtBoxTask.Text = dgvTaskList.Rows[rowIndex].Cells[1].Value.ToString();
            txtBoxTaskDescription.Text = dgvTaskList.Rows[rowIndex].Cells[2].Value.ToString();
            cmbStatus.Text = dgvTaskList.Rows[rowIndex].Cells[3].Value.ToString();
            dateTimePicker.Text = dgvTaskList.Rows[rowIndex].Cells[4].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            t.TaskID = Convert.ToInt32(txtBoxTaskID.Text);
            bool success = t.Delete(t);
            if (success = true)
            {
                MessageBox.Show("Usunięto zadanie");
                DataTable dt = t.Select();
                dgvTaskList.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Nie udało sie usunąć zadania");
            }
        }

        static string myconnstr = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        private void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtBoxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_Task WHERE Task LIKE '%"+keyword+"%' OR TaskDescription LIKE '%"+keyword+ "%' OR Status LIKE '%"+keyword+"%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvTaskList.DataSource = dt;
        }
    }
}
