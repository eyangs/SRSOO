using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SRSOO.IDAL;
using SRSOO.Util.Data;
using SRSOO.Util.Extension;


namespace SRSOO.SqlServerDAL
{
    public class StudentDAO:DataBase ,IStudent
    {
        public Student GetStudent(string id)
        {
            string sql = "select * from Student where id='{0}'".FormatWith(id);
            SqlDataReader dr = SqlHelper.ExecuteReader(ConStr, CommandType.Text, sql);
            if (dr.HasRows == false) return null;
            dr.Read();
            var stu = new Student(dr["Name"].ToString(), dr["Id"].ToString(), dr["Major"].ToString(), dr["Degree"].ToString());
            dr.Close();
            dr.Dispose();
        
           //访问数据库，获取选课信息
            var attends = new List<Section>();

           string sqll = @"select * from AttendSection where StudentNumber='{0}'".FormatWith(id);
                DataTable attendSec =SqlHelper.ExecuteDataset(ConStr,CommandType.Text,sqll).Tables[0];
            var secDao =new SectionDAO();
            foreach (DataRow r in attendSec.Rows)
            {
                attends.Add(secDao.GetSection(r["SectionNumber"].ConvertToIntBaseZero()));

            }


            //attends.Add(new Section(0, "", "", null, "", 0));
            stu.Attends =attends;
            //访问数据库把成绩单读出来
            return stu;

        }
    }
}
