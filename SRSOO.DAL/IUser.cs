using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRSOO.IDAL
{
    public interface IUser
    {
        void Insert(User user);

        User GetUser(int id);

        User GetUser(string userName);
    }  
}
