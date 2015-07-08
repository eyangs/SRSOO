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
    public class SectionDAO : DataBase, ISection
    {
        public Section GetSecion(int SectionNumber)
        {
            //从数据库读取数据
            string sql = "select * from Section where SectionNumber={0}".FormatWith(SectionNumber);
            SqlDataReader dr = SqlHelper.ExecuteReader(ConStr, CommandType.Text, sql);
            if (dr.HasRows == false) return null;
            dr.Read();
            var courseDao = new CousrseDAO();
            var sec = new Section(dr["SectionNumber"].ConvertToIntBaseZero(),
                                   dr["DayOfWeek"].ToString(),
                                   dr["TimeOfDay"].ToString(),
                                   courseDao.GetCourse(dr["RepresentedCourse"].ConvertToString()),
                                   dr["Room"].ToString(),
                                   dr["Capacity"].ConvertToIntBaseZero());
            dr.Close();
            dr.Dispose();

            return sec;

        }

        internal Section GetSection(int p)
        {
            throw new NotImplementedException();
        }
    }
}