using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam_m2_w4.Models
{
    public class Azk
    {
        public int AzkId { get; set; }
        public string Name { get; set; } = null!;
        public string Inn { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime RegisterDate { get; set; }
        public string RegisterUserInfo { get; set; }
        public string Phone { get; set; }
    }
}
