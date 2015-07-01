using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRSOO.BLL
{
    Public class StudentService
    {
        private static Istudent studentDao=DataAccess.CreateStudentDAO();
        public static Student LoadStudentInfo(string id)
        {
            return studentDao.GetStudent(id);
        }
    }
}
