using exam_m2_w4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam_m2_w4.Interface
{
    public interface IShiftDatas
    {
        List<ShiftDatas> GetShiftDatas();
        ShiftDatas GetShiftDatasById(int shiftid);
        bool CreateShiftDatas(ShiftDatas data);
        bool UpdateShiftDatas(ShiftDatas data);
        bool DeleteShiftDatas(int shiftid);
    }
}
