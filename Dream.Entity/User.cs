using System;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace Dream.Entity
{
	public class User
	{
		public int User_ID { get; set; }
		public String User_Name { get; set; }
		public String User_Address { get; set; }
		public String User_Email { get; set; }
		public String User_Tel { get; set; }
		public String User_Sex { get; set; }
		public String User_Birthday { get; set; }
		public String User_Race { get; set; }
		public String User_Degree { get; set; }
		// base64 
		public String User_Image { get; set; } 
		public String User_Password { get; set; }
	
		public String User_ImageType { get; set; }
		public User()
		{

		}


	}
}