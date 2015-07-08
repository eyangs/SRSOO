﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRSOO.BLL;
using SRSOO.Util;
using SRSOO.Util.Extension;
public partial class pages_selectCourse : WebBasePage//WebBasePage为自己建立的类（界面基类 含公用方法等），此处继承
{   
    public string user_name = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        User user = (User)Session["CurrentUser"];
        user_name = user.UserName;
        if (Request.Params["Action"].ConvertToString() == "loadStudentInfo")//ConvertToString为自己建立的扩展方法 可将对象转化为字符串
        {
            var stu = StudentService.LoadStudentInfo(CurrentUser.RelatedPerson);
            var q = from s in stu.Attends
                    select new
                    {
                        id = s.SectionNumber,
                        text = "{0} {1}".FormatWith(s.RepresentedCourse.CourseName, s.TimeOfDay, s.Room)
                    };
            var stuView = new
            {
                Id = stu.Id,
                Name = stu.Name,
                Attends = q.ToList()
            };
            string jsonResult = JSONHelper.ToJson(stuView);
            Response.Write(jsonResult);
            Response.End();
        }
        else if (Request.Params["Action"].ConvertToString() == "LoadSchedule")
        {
            var schedule = ScheduleService.LoadSchedule("SP2009");
            var q = from item in schedule.GetSortedSections()
                    select new
                    {
                        id = item.RepresentedCourse.CourseNumber,
                        text = "{0} {1} {2}".FormatWith(item.RepresentedCourse.CourseName, item.TimeOfDay, item.Room)
                    };
            string jsonResult = JSONHelper.ToJson(q.ToList());
            Response.Write(jsonResult);
            Response.End();
        }
        else if (Request.Params["Action"].ConvertToString() == "loadResigistion")
        {           
            var stu = StudentService.LoadStudentInfo(CurrentUser.RelatedPerson);
            var q = from s in stu.Attends
                    select new
                        {
                            id = s.SectionNumber,
                            text = "{0} {1}".FormatWith(s.RepresentedCourse.CourseName, s.TimeOfDay, s.Room)
                        };
            var stuView = new
            {
                Attends = q.ToList()
            };
            string jsonResult = JSONHelper.ToJson(stuView);
            Response.Write(jsonResult);
            Response.End();
        }
    }
}