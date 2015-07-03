using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRSOO.BLL;
using SRSOO.Util;
using SRSOO.Util.Extension;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"].ConvertToString() == "Login")
        {
            string jsonResult;
            string userName = Request.GetStringValueInForm("username");
            string password = Request.GetStringValueInForm("password");
            
            string message;
            User user = UserService.UserLogin(userName, password, out message);
            if (user!=null)
            {
                Session["CurrentUser"] = user;
                jsonResult = JSONHelper.GetJsonForSuccess();
            }
            else
            {
                jsonResult = JSONHelper.GetJsonForSomeReson(message);
            }
            Response.Write(jsonResult);
            Response.End();
        }
    }
   
}