using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektST1_w65453.ToDoClasses
{
    internal class taskClass
    {
        //gettery i settery
        public int TaskID { get; set; }
        public string Task { get; set; }
        public string TaskDescription { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }

        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        //wybór danych z bazy
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tbl_Task";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //wprowadzanie danych do bazy
        public bool Insert(taskClass t)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "INSERT INTO tbl_Task (Task, TaskDescription, Status, Date) VALUES (@Task, @TaskDescription, @Status, @Date)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Task", t.Task);
                cmd.Parameters.AddWithValue("@TaskDescription", t.TaskDescription);
                cmd.Parameters.AddWithValue("@Status", t.Status);
                cmd.Parameters.AddWithValue("@Date", t.Date);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //aktualizowanie danych w bazie
        public bool Update(taskClass t)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "UPDATE tbl_Task SET Task=@Task, TaskDescription=@TaskDescription, Status=@Status, Date=@Date WHERE TaskID=@TaskID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Task", t.Task);
                cmd.Parameters.AddWithValue("@TaskDescription", t.TaskDescription);
                cmd.Parameters.AddWithValue("@Status", t.Status);
                cmd.Parameters.AddWithValue("@Date", t.Date);
                cmd.Parameters.AddWithValue("@TaskID", t.TaskID);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        //usuwanie danych z bazy
        public bool Delete(taskClass t)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                string sql = "DELETE FROM tbl_Task WHERE TaskID=@TaskID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TaskID", t.TaskID);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
    }
}
