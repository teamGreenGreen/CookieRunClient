using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DTO.AuthServer
{
    public class LoginAccountReq
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginAccountRes : ErrorCodeDTO
    {
        public Int64 UserId { get; set; }
        public string AuthToken { get; set; }
    }
}
