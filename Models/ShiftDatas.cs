using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam_m2_w4.Models
{
    public class ShiftDatas
    {
        public int ShiftId { get; set; }
        public int AzsId { get; set; }
        public int AzkId { get; set; }
        public DateTime SenderDate { get; set; }
        public int StatusCode { get; set; }
        public string XmlReport { get; set; }
        public string Error { get; set; }
    }
}
