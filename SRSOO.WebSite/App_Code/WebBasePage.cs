using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public class WebBasePage: System.Web.UI.Page
{
    public User CurrentUser//用CurrentUser得到session
    {
        get
        {
          
            if (Session["CurrentUser"] == null)
            {
                //转到登录页面
                return null;
            }
            else
            {
                return Session["CurrentUser"] as User;
            }
        }
    }
}
