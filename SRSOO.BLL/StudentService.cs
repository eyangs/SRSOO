using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRSOO.BLL
{
    public class ScheduleService
    {

        private static IStudent studentDao = DataAccess.CreateStudentDAO();
        public static Student LoadSchedule(string id)
        {
            return studentDao.GetStudent(id);
        }
    }
}
