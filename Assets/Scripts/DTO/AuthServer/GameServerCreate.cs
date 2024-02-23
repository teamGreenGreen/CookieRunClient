using System;

namespace Assets.Scripts.DTO
{

    public class GameServerCreateReq
    {
        public Int64 UserId { get; set; }
        public string UserName { get; set; }
    }

    public class GameServerCreateRes : ErrorCodeDTO
    {
        public string SessionId { get; set; }
        public Int64 Uid { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}