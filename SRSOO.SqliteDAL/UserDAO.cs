using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SRSOO.IDAL;
using SRSOO.Util.Data;
using SRSOO.Util.Extension;

namespace SRSOO.SqliteDAL
{
    public class DataBase
    {
        
    }
    
    public class UserDAO: DataBase,IUser
    {
        public void Insert(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
