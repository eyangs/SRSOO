using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRSOO.IDAL
{
    public interface ISection
    {
        void Insert(Section section);

        Section GeSection(int id);

        Section GetSection(string sectionNumber);
    }  
}
