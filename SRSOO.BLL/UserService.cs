using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRSOO.IDAL;

namespace SRSOO.BLL
{
    public class UserService
    {
        private static IUser userDao = DataAccess.CreateUserDAO();
        public static User UserLogin(string userName, string passWord, out string message)
        {
            User result = null;
            User user = userDao.GetUser(userName);
            if (user == null)
            {
                message = "用户名不存在";
            }
            else if (user.PassWord != passWord)
            {
                message = "用户密码不正确";
            }
            else
            {
                result = user;
                message = "登录成功！";
            }
            return result;
        }
    }
}
