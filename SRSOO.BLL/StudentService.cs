using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRSOO.IDAL;

namespace SRSOO.BLL
{
    public class StudentService
    {
        private static IStudent studentDAO = DataAccess.CreateStudentDAO();
        public static Student LoadStudentInfo(string id)
        {
            return studentDAO.GetStudent(id);
        }


    }
}
