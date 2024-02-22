using System.ComponentModel;
using Assets.Scripts.DTO;

namespace Assets.Scripts.DTO.GameServer
{
    //  요청 데이터
    public class FriendDeleteReq
    {
        public string FriendName { get; set; }
    }
    // 응답 데이터
    public class FriendDeleteRes : ErrorCodeDTO
    {

    }
}