using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DTO
{
    public class RankUpdateReq
    {
        public string? UserName { get; set; }
        public double Score { get; set; }
    }
    public class RanksLoadReq
    {
        public int Page { get; set; }
        public int PlayerNum { get; set; }
    }
    public class RanksLoadRes : ErrorCodeDTO
    {
        public string[]? Ranks { get; set; }
    }
    public class RankGetRes : ErrorCodeDTO
    {
        public string Rank { get; set; }
    }
    public class RankSizeRes : ErrorCodeDTO
    {
        public long Size { get; set; }
    }
}
