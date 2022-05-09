using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using Dream.Entity;
using System.IO;
using System.Drawing;

namespace Dream.Service
{
    public class TestInfoService
    {
        public TestInfoService()
        {

        }

        public String AddTestInfo(TestInfo n)
        {

            SqlConnection sqlConnection = DbHelper.instance().GetSqlConnection();
            String query = "INSERT INTO [testinfo] (testinfotitle,testinfocontent,testinfotime) VALUES (@title,@content,@time);";

            SqlCommand command = new SqlCommand(query, sqlConnection);

            //FileStream fstream = new FileStream(u.Image, FileMode.Open, FileAccess.Read);
            //BinaryReader br = new BinaryReader(fstream);
            //imageBT = br.ReadBytes((int)fstream.Length);

            command.Parameters.AddWithValue("@title", n.testinfotitle);
            command.Parameters.AddWithValue("@content", n.testinfocontent);
            command.Parameters.AddWithValue("@time", n.testinfotime);


            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            command.ExecuteNonQuery();

            sqlConnection.Close();
            return "ok";

        }

        
        public String listTestInfo()
        {
            try
            {
                SqlConnection sqlConnection = DbHelper.instance().GetSqlConnection();
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                String sql = @"SELECT *  FROM [testinfo]
";
                DataSet dataSet = new DataSet();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
                sqlConnection.Close();
                sqlDataAdapter.Fill(dataSet);
                return JsonConvert.SerializeObject(dataSet);
            }
            catch (Exception e)
            {
                return e.ToString();
            }


        }


        public String ResetPassword(String uid, String password)
        {

            String queryString = $"UPDATE [user] SET User_Password={password} WHERE User_ID={uid} ";
            SqlConnection sqlConnection = DbHelper.instance().GetSqlConnection();
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }


            try
            {
                int i = command.ExecuteNonQuery();
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            finally
            {
                sqlConnection.Close();

            }


        }
    }
}
