using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SRSOO.IDAL;
using SRSOO.Util.Data;
using SRSOO.Util.Extension;

namespace SRSOO.SqlServerDAL
{
    
    public class DataBase
    {
        public static string ConStr
        {
            get { return @"Data Source=(localdb)\Projects;Initial Catalog=SRSDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False"; }
        }

        public static SqlConnection Connection
        {
            get { return new SqlConnection(ConStr);}
        }
    }
    
    
    
    public class UserDAO: DataBase, IUser
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
            string sql = "select * from [User] where UserName='{0}'".FormatWith(userName.Trim());
            SqlDataReader dr = SqlHelper.ExecuteReader(ConStr, CommandType.Text, sql);
            if (dr.HasRows == false) return null;
            dr.Read();
            User user = new User();
            user.UserName = dr["UserName"].ToString();
            user.PassWord = dr["Password"].ToString();
            user.RelatedPerson = dr["RelatedPerson"].ToString();
            user.RelatedPersonType = dr["RelatedPersonType"].ToString();
            dr.Close();
            dr.Dispose();
            return user;
        }
    }
}
