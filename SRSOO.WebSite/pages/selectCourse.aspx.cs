using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRSOO.BLL;
using SRSOO.Util;
using SRSOO.Util.Extension;
public partial class pages_selectCourse : WebBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"].ConvertToString() == "LoadSchedule")
        {
            var schedule = ScheduleService.LoadSchedule("SP2009");
            var q = from item in schedule.GetSortedSections()
                select new
                {
                    id = item.RepresentedCourse.CourseNumber,
                    text = "{0} {1} {2} {3}".FormatWith(item.RepresentedCourse.CourseName, item.TimeOfDay,item.Room,item.SectionNumber)
                };
            string jsonResult = JSONHelper.ToJson(q.ToList());

            Response.Write(jsonResult);
            Response.End();
        }
        else if (Request.Params["Action"].ConvertToString() == "LoadStudentInfo")
        {
           // User u =Session["CurrentUser"} as User;
            var stu = StudentService.LoadStudentInfo(CurrentUser.RelatedPerson);
            //生成viewmodel
            //匿名对象new{}
            var q = from s in stu.Attends 
                    select new
                    {
                        id = s.SectionNumber,
                        text = "{0} {1} {2} {3}".FormatWith(s.RepresentedCourse.CourseName, s.TimeOfDay, s.Room,s.SectionNumber)
                    };

            var stuView = new{
                Id = stu.Id,
                Name = stu.Name,
                Attends =q.ToList(),
                Num =stu.Attends.Count
            };
            string jsonResult =JSONHelper.ToJson(stuView);
            Response.Write(jsonResult);
            Response.End();
                        
        }

        
    }
}