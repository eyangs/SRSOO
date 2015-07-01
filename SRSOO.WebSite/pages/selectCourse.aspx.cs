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
                    text = "{0} {1} {2}".FormatWith(item.RepresentedCourse.CourseName, item.TimeOfDay,item.Room)
                };
            string jsonResult = JSONHelper.ToJson(q.ToList());

            Response.Write(jsonResult);
            Response.End();
        }
    }
}