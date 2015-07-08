using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRSOO.IDAL;

namespace SRSOO.BLL
{
    public class StudentService
    {
        private static IStudent studentDao = DataAccess.CreateStudentDAO();
        public static Student LoadStudentInfo(string id)
        {
            return studentDao.GetStudent(id);
        }
    }
}
