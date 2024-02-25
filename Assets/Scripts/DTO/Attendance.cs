using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DTO
{
    public class AttendanceRes : ErrorCodeDTO
    {
        public int RemainDays { get; set; }
        public int AttendanceCount { get; set; }
        public string[] Rewards { get; set; }
    }
    public class AttendanceReq
    {

    }
}
