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
   public class CousrseDAO: DataBase, ICourse
    {
       public void Insert(Course course)
       {
           throw new NotImplementedException();
       }

       public Course GetCourse(string courseNumber)
       {
           string sql = "select * from Course where CourseNumber='{0}'".FormatWith(courseNumber);
           SqlDataReader dr = SqlHelper.ExecuteReader(ConStr, CommandType.Text, sql);
           if (dr.HasRows == false) return null;
           dr.Read();
           var course = new Course(dr["CourseNumber"].ToString(), dr["CourseName"].ToString(), dr["Credit"].ConvertToDouble());
           dr.Close();
           dr.Dispose();
           return course;
       }

       public void GetPreRequisites(Course course)
       {
           string sql = "select * from Prerequisite where CourseNumber='{0}'".FormatWith(course.CourseNumber);
           DataTable dt = SqlHelper.ExecuteDataset(ConStr, CommandType.Text, sql).Tables[0];
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               course.AddPrerequisite(GetCourse(dt.Rows[i]["Prerequisite"].ConvertToString()));
           }
       }
    }
}
