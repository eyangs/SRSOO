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
    class SectionDAO:DataBase,ISection
    {
        public Section GetSection(int sectionNumber)
        {
            string sql = "select * from Section where SectionNumber={0}".FormatWith(sectionNumber);
            SqlDataReader dr = SqlHelper.ExecuteReader(ConStr, CommandType.Text, sql);
            if (dr.HasRows == false) return null;
            dr.Read();
            var courseDao = new CourseDAO();
            var sec = new Section(dr["SectionNumber"].ConvertToIntBaseZero(),
                                  dr["DayOfWeek"].ToString(),
                                  dr["TimeOfDay"].ToString(),
                                  courseDao.GetCourse(dr["RepresentedCourse"].ConvertToString()),
                                  dr["Room"].ToString(),
                                  dr["SeatingCapacity"].ConvertToIntBaseZero());
            dr.Close();
            dr.Dispose();

            return sec;
        }


        public void Insert(Section section)
        {
            throw new NotImplementedException();
        }

        public Section GeSection(int id)
        {
            throw new NotImplementedException();
        }

        public Section GetSection(string sectionName)
        {
            throw new NotImplementedException();
        }
    }
}
