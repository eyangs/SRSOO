using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRSOO.IDAL
{
    public interface ISection
    {
        void Insert(Section section);

        Section GetSection(int id);

      Section GetSection(string sectionName);
    }  
}
