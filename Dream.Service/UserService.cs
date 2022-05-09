using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using Dream.Entity;
using System.IO;
using System.Drawing;

namespace Dream.Service
{
    public class UserService
    {
        public UserService()
        {

        }

        public String RegisterUser(User u)
        {

            SqlConnection sqlConnection = DbHelper.instance().GetSqlConnection();
            String query = "INSERT INTO [user] (User_Name,User_Address,User_Email,User_Tel,User_Sex,User_Birthday,User_Race,User_Degree,User_Image,User_Password,User_ImageType)" +
                "output inserted.User_ID" +
                " VALUES (@name,@address,@email,@tel,@sex,@birthday,@race,@degree,@image,@password,@imageType);";

            SqlCommand command = new SqlCommand(query, sqlConnection);



            //FileStream fstream = new FileStream(u.Image, FileMode.Open, FileAccess.Read);
            //BinaryReader br = new BinaryReader(fstream);
            //imageBT = br.ReadBytes((int)fstream.Length);

            command.Parameters.AddWithValue("@name", u.User_Name);
            command.Parameters.AddWithValue("@address", Utils.checkNull(u.User_Address));

            command.Parameters.AddWithValue("@email", u.User_Email);
            command.Parameters.AddWithValue("@tel", Utils.checkNull(u.User_Tel));
            command.Parameters.AddWithValue("@sex", Utils.checkNull(u.User_Sex));
            command.Parameters.AddWithValue("@birthday", Utils.checkNull(u.User_Birthday));

            command.Parameters.AddWithValue("@race", Utils.checkNull(u.User_Race));
            command.Parameters.AddWithValue("@degree", Utils.checkNull(u.User_Degree));



            if (u.User_Image == null || u.User_Image == "")
            {
                byte[] a = new byte[10];
                command.Parameters.AddWithValue("@image", a);
                command.Parameters.AddWithValue("@imageType", "");
            }
            else
            {
                String[] imageParts = u.User_Image.Split(',');
                byte[] imageBytes = Convert.FromBase64String(imageParts[1]);
                command.Parameters.AddWithValue("@image", imageBytes);
                command.Parameters.AddWithValue("@imageType", imageParts[0] + ",");
            }


            command.Parameters.AddWithValue("@password", Utils.checkNull(u.User_Password));
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            Object id = command.ExecuteScalar();


            sqlConnection.Close();

            // Check Error
            // if (result < 0)
            // {
            //     Console.WriteLine("Error inserting User into Database!");

            //     return "Error inserting User into Database!";
            // }

            return id.ToString();

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

            queryString = @"SELECT * FROM [user] WHERE User_Name = @name AND User_Password = @password";

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
                    u.User_ImageType = (String)reader["User_ImageType"].ToString();
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

        public String listUser()
        {
            try
            {
                SqlConnection sqlConnection = DbHelper.instance().GetSqlConnection();
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                String sql = @"
SELECT TOP (1000) [User_ID]
      ,[User_Name]
      ,[User_Address]
      ,[User_Email]
      ,[User_Tel]
      ,[User_Sex]
      ,[User_Birthday]
      ,[User_Race]
      ,[User_Degree]
      ,[User_Image]
      ,[User_Password]
      ,[User_ImageType]
  FROM [Dream].[dbo].[user]
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
