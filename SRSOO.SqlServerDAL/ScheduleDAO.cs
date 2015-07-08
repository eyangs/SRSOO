using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web;
using SRSOO.IDAL;
using SRSOO.Util.Data;
using SRSOO.Util.Extension;

namespace SRSOO.SqlServerDAL
{
    public class ScheduleDAO : DataBase, ISchedule
    {
        /// <summary>
        /// 此处为了省事，没有连接数据库，直接读文件
        /// </summary>
        /// <param name="semester"></param>
        /// <returns></returns>
        public ScheduleOfClasses GetScheduleOfClasses(string semester)
        {
            var result = new ScheduleOfClasses(semester);
            StreamReader reader = null;
            try
            {
                var scheduleFile = HttpRuntime.AppDomainAppPath + @"data\Soc_SP2009.dat";
                // Open the file. 
                reader = new StreamReader(new FileStream(scheduleFile, FileMode.Open));

                //  Read first line from input file.
                string line = reader.ReadLine();

                //  Keep reading lines until there aren't any more.
                while (line != null)
                {

                    // We'll use the Split() method of the String class to split
                    // the line we read from the file into substrings using tabs 
                    // as the delimiter.

                    string[] strings = line.Split('\t');

                    // Now assign the value of the fields to the appropriate
                    // substring

                    string courseNumber = strings[0];
                    string sectionValue = strings[1];
                    string dayOfWeek = strings[2];
                    string timeOfDay = strings[3];
                    string room = strings[4];
                    string capacityValue = strings[5];

                    // We need to convert the sectionNumber and capacityValue
                    // Strings to ints

                    int sectionNumber = Convert.ToInt32(sectionValue);
                    int capacity = Convert.ToInt32(capacityValue);

                    // Look up the Course object in the Course Catalog.
                    var courseDao = new CourseDAO(); 
                    Course c = courseDao.GetCourse(courseNumber);
                    courseDao.GetPreRequisites(c);
                    // Schedule the Section and add it to the Dictionary.

                    Section s = c.ScheduleSection(sectionNumber, dayOfWeek,
                                          timeOfDay, room, capacity);
                    result.AddSection(s);

                    line = reader.ReadLine();
                }
            }  //  End of try block
            catch (FileNotFoundException f)
            {
                Console.WriteLine(f);
            }
            catch (IOException i)
            {
                Console.WriteLine(i);
            }
            finally
            {
                //  Close the input stream.
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return result;
        }
    }
}
