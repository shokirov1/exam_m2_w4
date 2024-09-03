using exam_m2_w4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam_m2_w4.Interface
{
    public interface IOnlineDatas
    {
        List<OnlineDatas> GetOnlineDatas();
        OnlineDatas GetOnlineDataById(int id);
        bool CreateOnlineDatas(OnlineDatas onlineDatas);
        bool UpdateOnlineDatas(OnlineDatas onlineDatas);
        bool DeleteOnlineDatas(int id);
    }
}
