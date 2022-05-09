using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using Dream.Entity;
using System.IO;
using System.Drawing;

namespace Dream.Service
{
    public class StudentService
    {
        public StudentService()
        {

        }

        public String AddStudent(Student n)
        {

            SqlConnection sqlConnection = DbHelper.instance().GetSqlConnection();
            String query = @"INSERT INTO [student] (uname,stuname,stubirthday,stumobile,stumail,stuage,stunation,stuhomephone,stusex,subname) 
                        VALUES (@uname,@stuname,@stubirthday,@stumobile,@stumail,@stuage,@stunation,@stuhomephone,@stusex,@subname);";

            SqlCommand command = new SqlCommand(query, sqlConnection);

            //FileStream fstream = new FileStream(u.Image, FileMode.Open, FileAccess.Read);
            //BinaryReader br = new BinaryReader(fstream);
            //imageBT = br.ReadBytes((int)fstream.Length);

            command.Parameters.AddWithValue("@uname", n.uname);
            command.Parameters.AddWithValue("@stuname", n.stuname);
            command.Parameters.AddWithValue("@stubirthday", n.stubirthday);
            command.Parameters.AddWithValue("@stumobile", n.stumobile);
            command.Parameters.AddWithValue("@stumail", n.stumail);
            command.Parameters.AddWithValue("@stuage", n.stuage);
            command.Parameters.AddWithValue("@stunation", n.stunation);
            command.Parameters.AddWithValue("@stuhomephone", n.stuhomephone);
            command.Parameters.AddWithValue("@stusex", n.stusex);
            command.Parameters.AddWithValue("@subname", n.subname);


            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            command.ExecuteNonQuery();

            sqlConnection.Close();
            return "ok";

        }

        public User queryStudent(String name, String password)
        {
            string queryString = @"SELECT 
                [User_ID]
                ,[User_Name]
                ,User_Address
                ,User_Email
                ,User_Tel
                ,User_Sex
                ,User_Birthday
                ,User_Race
                ,User_Degree
                ,User_Image
                ,User_Password
                ,User_ImageType
                FROM [user] WHERE User_Name = @name AND User_Password = @password";

            SqlConnection sqlConnection = DbHelper.instance().GetSqlConnection();
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@password", password);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                User u = new User();
                if (reader.Read())
                {
                    u.User_ID = (int)reader["User_ID"];
                    u.User_Name = (String)reader["User_Name"];
                    u.User_Address = reader["User_Address"].ToString();
                    u.User_Email = (String)reader["User_Email"];
                    u.User_Tel = (String)reader["User_Tel"].ToString();
                    u.User_Sex = (String)reader["User_Sex"].ToString();
                    u.User_Birthday = (String)reader["User_Birthday"].ToString();
                    u.User_Race = (String)reader["User_Race"].ToString();
                    u.User_Degree = (String)reader["User_Degree"].ToString();
                    byte[] img = (byte[])reader["User_Image"];
                    u.User_Image = Convert.ToBase64String(img).ToString();
                    u.User_Password = (String)reader["User_Password"].ToString();
                }
                return u;
            }
            finally
            {
                // Always call Close when done reading.
                reader.Close();
            }
            return null;
        }

        public String listStudent()
        {
            try
            {
                SqlConnection sqlConnection = DbHelper.instance().GetSqlConnection();
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                String sql = @"SELECT *  FROM [student]
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

        public String removeStudent(String uname)
        {
            SqlConnection sqlConnection = DbHelper.instance().GetSqlConnection();

            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                String sql = $"Delete FROM [student] WHERE uname='{uname}'";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                int i = cmd.ExecuteNonQuery();
                sqlConnection.Close();
             
                return i.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }finally
            {
                sqlConnection.Close();

            }


        }
        


        public String queryStudent(String uname)
        {

            String queryString = $"SELECT * FROM [student] WHERE uname='{uname}'";
            SqlConnection sqlConnection = DbHelper.instance().GetSqlConnection();
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            try
            {
                DataSet dataSet = new DataSet();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(queryString, sqlConnection);
                sqlConnection.Close();
                sqlDataAdapter.Fill(dataSet);
                return JsonConvert.SerializeObject(dataSet);
             
            }
            catch (Exception e)
            {
                throw ;
            }
            finally
            {
                sqlConnection.Close();

            }
        }


        public String queryAllStudent()
        {

            String queryString = $"SELECT * FROM [student]";
            SqlConnection sqlConnection = DbHelper.instance().GetSqlConnection();
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            try
            {
                DataSet dataSet = new DataSet();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(queryString, sqlConnection);
                sqlConnection.Close();
                sqlDataAdapter.Fill(dataSet);
                return JsonConvert.SerializeObject(dataSet);

            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();

            }


        }
    }
}
