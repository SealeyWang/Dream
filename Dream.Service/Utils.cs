using System;

namespace Dream.Service
{
	public class Utils
    {
    
        public static Object checkNull (Object obj)
        {
            if(obj == null)
            {
                return DBNull.Value;
            }
            return obj;
            
        }

    }
}

