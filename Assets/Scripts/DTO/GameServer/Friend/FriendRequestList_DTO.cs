using System.Collections.Generic;
using System.ComponentModel;
using Assets.Scripts.DTO;
using Assets.Scripts.DAO.GameServer;

namespace Assets.Scripts.DTO.GameServer
{
    //  요청 데이터

    // 응답 데이터
    public class FriendRequestListRes : ErrorCodeDTO
    {
        public IEnumerable<FriendRequestElement> FriendRequestList { get; set; } // DAO의 친구 신청 요소로 이루어진 List를 반환
    }
}