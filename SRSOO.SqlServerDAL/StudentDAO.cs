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
    
    public class StudentDAO:DataBase,IStudent
    {
        public student Getstudent(string id)
        {
            string sql = "select * from Student where id='{0}'".FormatWith(id);
            SqlDataAdapter dr = SqlHelper.ExecuteReader(ConStr,CommandType.Text,)
            if (dr.HasRows == false) return null;
            dr.Read();
            var stu = new Student(dr["Name"].ToString(),dr["Id"].ToString(),
                dr["Major"].ToString(),dr["Degree"].ToString());
            dr.Close();
            dr.Dispose();
            //访问数据库，获取选课信息
            var attends = new List<Section>();
            string sql = @"select * from AttendSection where StudentNumber='{0}'".FormatWith(id);
            DataTable attedsSec = SqlHelper.ExecuteDataset(ConStr,CommandType.Text,sql1).Tables[0];
            var secDao = new SectionDAO();
            foreach (var r in attendSec.Rows)
            {
                attends.Add(secDao.GetSection(r["SectionNumber"].ConvertToIntBaseZero()));
            }
            stu.Attends = attends;
            //访问数据库把成绩单读过来
            return stu;
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
            dr.Close();
            dr.Dispose();
            return user;
        }
    }
}
