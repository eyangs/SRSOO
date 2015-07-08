using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SRSOO.IDAL
{
    public class DataAccess
    {
        //private static readonly string AssemblyName = ConfigurationManager.AppSettings["DAL"];
        private static readonly string AssemblyName = "SRSOO.SqlServerDAL";
        public static IUser CreateUserDAO()
        {
            string className = AssemblyName + "." + "UserDAO";
            return (IUser)Assembly.Load(AssemblyName).CreateInstance(className);
        }

        public static ICourse CreateCourseDAO()
        {
            string className = AssemblyName + "." + "CourseDAO";
            return (ICourse)Assembly.Load(AssemblyName).CreateInstance(className);
        }
        public static ISchedule CreateScheduleDAO()
        {
            string className = AssemblyName + "." + "ScheduleDAO";
            return (ISchedule)Assembly.Load(AssemblyName).CreateInstance(className);
        }
        public static IStudent CreateStudentDAO()
        {
            string className = AssemblyName + "." + "StudentDAO";
            return (IStudent)Assembly.Load(AssemblyName).CreateInstance(className);
        }
    }
}
