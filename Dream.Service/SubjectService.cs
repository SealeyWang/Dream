using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using Dream.Entity;
using System.IO;
using System.Drawing;

namespace Dream.Service
{
    public class SubjectService
    {
        public SubjectService()
        {

        }

        public String AddSubject(Subject s)
        {

            SqlConnection sqlConnection = DbHelper.instance().GetSqlConnection();
            String query = "INSERT INTO [subject] (subname,subnumber,subcost,subinfo) VALUES (@name,@number,@cost,@info);";

            SqlCommand command = new SqlCommand(query, sqlConnection);



            //FileStream fstream = new FileStream(u.Image, FileMode.Open, FileAccess.Read);
            //BinaryReader br = new BinaryReader(fstream);
            //imageBT = br.ReadBytes((int)fstream.Length);

            command.Parameters.AddWithValue("@name", s.subname);
            command.Parameters.AddWithValue("@number", s.subnumber);

            command.Parameters.AddWithValue("@cost", s.subcost);
            command.Parameters.AddWithValue("@info", s.subinfo);


            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            command.ExecuteNonQuery();

            sqlConnection.Close();
            return "ok";

        }

        public User queryUser(String name, String password)
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

        public String listSubject()
        {
            try
            {
                SqlConnection sqlConnection = DbHelper.instance().GetSqlConnection();
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                String sql = @"SELECT *  FROM [subject]
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
