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
        public static string ConStr
        {
            get { return @"Data Source=(LocalDB)\v11.0;AttachDbFilename=I:\SRSOO\SRSOO.SqliteDAL\database\SRSDB.mdf;Integrated Security=True"; }
        }

        public static SqlConnection Connection
        {
            get { return new SqlConnection(ConStr); }
        }


        public Student GetStudent(string id)
        {
            string sql = "select * from Student id='{0}'".FormatWith(id);
            SqlDataReader dr = SqlHelper.ExecuteReader(ConStr, CommandType.Text, sql);
            if (dr.HasRows == false) return null;
            dr.Read();
            var stu = new Student(dr["Name"].ToString(),
                dr["Id"].ToString(),
                dr["Major"].ToString(),
                dr["degree"].ToString()
                );
            dr.Close();
            dr.Dispose();
            //
            var attends = new List<Section>();
            string sql1="select * from AttendSection where StudentNumber='{0}'".FormatWith(id);
            DataTable attendSec = SqlHelper.ExecuteDataset(ConStr,CommandType.Text,sql1).Tables[0];
            
            var secDao = new SectionDAO();
            foreach(DataRow r in attendSec.Rows)
            {
                attends.Add(secDao.GetSection(r["SectionNumber"].ConvertToIntBaseZero()));

            
            }
            stu.Attends=attends;
            return stu;
           
        }
    }
    

        
   
}
