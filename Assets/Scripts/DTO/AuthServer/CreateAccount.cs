using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DTO.AuthServer
{
    public class CreateAccountReq
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CreateAccountRes : ErrorCodeDTO
    {
    }
}
