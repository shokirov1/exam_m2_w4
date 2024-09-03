using exam_m2_w4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam_m2_w4.Interface
{
    public interface IAzs
    {
        List<Azs> GetAzs();
        Azs GetAzsById(int azsid);
        bool CreateAzs(Azs azs);
        bool UpdateAzs(Azs azs);
        bool DeleteAzs(int azsid);
    }
}
