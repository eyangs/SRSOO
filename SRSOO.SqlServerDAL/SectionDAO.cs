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
    
    public class SectionDAO:DataBase,ISection
    {
        public Section GetSection(int Id)
        {
            //应该从数据库读取section数据
            //var Course
            return new Section(1, "M", "8:10-10:00 PM", null, "GOVT101", 30);
        }

       
    }
}
