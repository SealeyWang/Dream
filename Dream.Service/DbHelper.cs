using System;
using System.Data;
using System.Data.SqlClient;

namespace Dream.Service
{
    class DbHelper
    {
        private static DbHelper mInstance;
        public SqlConnection sqlConnection;
        private DbHelper()
        {
            /**
            String cstr = "Server=localhost\\SQLEXPRESS;Database=Dream;uid=sa;pwd=sa;Trusted_Connection=True;";
            // String cstr = "";

            sqlConnection =
                new SqlConnection(cstr);
            */
       
        }




        public static DbHelper instance()
        {
            if (mInstance == null)
            {
                mInstance = new DbHelper();
            }
            return mInstance;
        }


        public SqlConnection GetSqlConnection()
        {
            String cstr = "Server=localhost\\SQLEXPRESS;Database=Dream;uid=sa;pwd=sa;Trusted_Connection=True;";

            return new SqlConnection(cstr);
        }

    }
}

