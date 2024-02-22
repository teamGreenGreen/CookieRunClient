using Assets.Scripts.DTO;
using System.ComponentModel;

namespace Assets.Scripts.DTO.GameServer
{
    //  요청 데이터
    public class FriendRequestReq
    {
        public string ToUserName { get; set; }
    }
    // 응답 데이터
    public class FriendRequestRes : ErrorCodeDTO
    {

    }
}