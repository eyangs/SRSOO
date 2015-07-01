using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRSOO.IDAL
{
    public interface ICourse
    {
        void Insert(Course course);

        Course GetCourse(string courseNumber);

        void GetPreRequisites(Course course);
    }  
}
