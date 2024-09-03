using exam_m2_w4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam_m2_w4.Interface
{
    public interface IAzk
    {
        List<Azk> GetAzk();
        Azk GetAzkById(int azkid);
        bool CreateAzk(Azk azk);
        bool UpdateAzk(Azk azk);
        bool DeleteAzk(int azkid);

    }
}
