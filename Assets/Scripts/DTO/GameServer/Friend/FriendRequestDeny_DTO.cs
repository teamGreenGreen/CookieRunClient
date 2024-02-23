using Assets.Scripts.DTO;
using System.ComponentModel;

namespace Assets.Scripts.DTO.GameServer
{
    //  요청 데이터
    public class FriendRequestDenyReq
    {
        public long RequestId { get; set; }
    }
    // 응답 데이터
    public class FriendRequestDenyRes : ErrorCodeDTO
    {

    }
}