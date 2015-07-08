using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRSOO.IDAL;

namespace SRSOO.BLL
{
    public class ScheduleService
    {

        private static ISchedule scheduleDao = DataAccess.CreateScheduleDAO();
        public static ScheduleOfClasses LoadSchedule(string semester)
        {
            return scheduleDao.GetScheduleOfClasses(semester);
        }
    }
}
